using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Enums;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Others;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface IAccountService
    {
        string LoginUser(LoginUserDto loginUserDto);
        void RegisterStudent(RegisterStudentDto registerStudentDto);
        void RegisterTeacherOrAdmin(RegisterTeacherOrAdminDto registerTeacherOrAdminDto);
        void RestoreUserPassword(RestoreUserPasswordDto restoreUserPasswordDto);
        UserInfoGetDto GetUserInfo(bool isStudent);
        void ChangePassword(string newPassword, bool isStudent);

    }

    public class AccountService : IAccountService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IPasswordHasher<Student> _passwordHasherStudent;
        private readonly IPasswordHasher<Teacher> _passwordHasherTeacher;
        private readonly IPasswordHasher<Admin> _passwordHasherAdmin;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextService _userContextService;
        public readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountService(ApiDbContext dbContext, 
            IPasswordHasher<Student> passwordHasherStudent, 
            IPasswordHasher<Teacher> passwordHasherTeacher, 
            IPasswordHasher<Admin> passwordHasherAdmin, 
            AuthenticationSettings authenticationSettings,
            IUserContextService userContextService, 
            IMapper mapper,
            IEmailService emailService)
        {
            _dbContext = dbContext;
            _passwordHasherStudent = passwordHasherStudent;
            _passwordHasherTeacher = passwordHasherTeacher;
            _passwordHasherAdmin = passwordHasherAdmin;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            var student = _dbContext.Student.FirstOrDefault(user => user.Login == loginUserDto.Login && user.IsDeleted == false);
            var teacher = _dbContext.Teacher.FirstOrDefault(user => user.Login == loginUserDto.Login && user.IsDeleted == false);
            var admin = _dbContext.Admin.FirstOrDefault(user => user.Login == loginUserDto.Login);

            PasswordVerificationResult result = new PasswordVerificationResult();
            User user = new User();

            if (student is null && teacher is null && admin is null)
                throw new BadRequestException("Wrong username or password!");

            else if (student is not null)
            {
                result = _passwordHasherStudent
                    .VerifyHashedPassword(student, student.Password, loginUserDto.Password);

                PasswordVerification(result);

                user = _mapper.Map<User>(student);
                user.UserRole = UserRoleEnum.Student;
            }

            else if (teacher is not null)
            {
                result = _passwordHasherTeacher
                    .VerifyHashedPassword(teacher, teacher.Password, loginUserDto.Password);

                PasswordVerification(result);

                user = _mapper.Map<User>(teacher);
                user.UserRole = UserRoleEnum.Teacher;
            }
            
            else if (admin is not null)
            {
                result = _passwordHasherAdmin
                    .VerifyHashedPassword(admin, admin.Password, loginUserDto.Password);

                PasswordVerification(result);

                user = _mapper.Map<User>(admin);
                user.UserRole = UserRoleEnum.Admin;
            }
            
            return _GenerateJwt(user);
        }

        public void RegisterStudent(RegisterStudentDto registerStudentDto)
        {
            var newStudent = new Student()
            {
                Name = registerStudentDto.Name,
                Surname = registerStudentDto.Surname,
                Email = registerStudentDto.Email,
                Login = registerStudentDto.Login,
            };
            var user = _mapper.Map<User>(newStudent);

            newStudent.Password = _passwordHasherStudent.HashPassword(newStudent, registerStudentDto.Password);

            _dbContext.Student.Add(newStudent);
            _dbContext.SaveChanges();
        }

        public void RegisterTeacherOrAdmin(RegisterTeacherOrAdminDto registerTeacherOrAdminDto)
        {
            if (registerTeacherOrAdminDto.UserRole == UserRoleEnum.Teacher)
            {
                var newTeacher = new Teacher()
                {
                    Name = registerTeacherOrAdminDto.Name,
                    Surname = registerTeacherOrAdminDto.Surname,
                    Email = registerTeacherOrAdminDto.Email,
                    Degree = registerTeacherOrAdminDto.Degree,
                    Position = registerTeacherOrAdminDto.Position,
                    Login = registerTeacherOrAdminDto.Login,
                };
                newTeacher.Password = _passwordHasherTeacher.HashPassword(newTeacher, registerTeacherOrAdminDto.Password);

                _dbContext.Teacher.Add(newTeacher);
                _dbContext.SaveChanges();
            }
            else if (registerTeacherOrAdminDto.UserRole == UserRoleEnum.Admin)
            {
                var newAdmin = new Admin()
                {
                    Login = registerTeacherOrAdminDto.Login,
                };
                newAdmin.Password = _passwordHasherAdmin.HashPassword(newAdmin, registerTeacherOrAdminDto.Password);

                _dbContext.Admin.Add(newAdmin);
                _dbContext.SaveChanges();
            }
            else
                throw new BadRequestException("Wrong role of created user!");

        }

        public void RestoreUserPassword(RestoreUserPasswordDto restoreUserPasswordDto)
        {
            var user = _dbContext.Student.FirstOrDefault(user => user.Email == restoreUserPasswordDto.Email && user.Login == restoreUserPasswordDto.Login);

            if (user == null) 
            { 
                throw new NotFoundException("User not found");
            }

            //var newPassword = RandomString(8);
            var newPassword = "Reset123!";
            user.Password = _passwordHasherStudent.HashPassword(user, newPassword);
            _dbContext.SaveChanges();

            EmailDto emailDto = new EmailDto()
            {
                To = user.Email,
                Subject = "System sprawozdan - zmiana hasla",
                Body = "Nowe haslo dla uzytkownika: " + user.Login + "\n<i>Haslo: </i><b>" + newPassword + "</b>"
            };
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string _GenerateJwt(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.UserRole}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }

        private void PasswordVerification(PasswordVerificationResult result)
        {
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Wrong username or password!");
        }

        //! Zwraca informacje o użytkowniku
        public UserInfoGetDto GetUserInfo(bool isStudent)
        {
            var userId = _userContextService.GetUserId;
            
            UserInfoGetDto info = new();

            if (isStudent == true)
            {
                var student = _dbContext.Student.FirstOrDefault(student => student.Id == userId);

                if (student != null)
                {
                    info.Login = student.Login;
                    info.Name = student.Name;
                    info.Surname = student.Surname;
                    info.Email = student.Email;
                }
                //else
                    //throw new NotFoundException("Couldn't find student with that id");

            }
            else
            {
                var teacher = _dbContext.Teacher.FirstOrDefault(teacher => teacher.Id == userId);

                if (teacher != null)
                {
                    info.Login = teacher.Login;
                    info.Name = teacher.Name;
                    info.Surname = teacher.Surname;
                    info.Email = teacher.Email;
                    info.Degree = teacher.Degree;
                    info.Position = teacher.Position;
                }
                //else
                    //throw new NotFoundException("couldn't find teacher with that id");

            }

            return info;
        }

        //! Zmienia hasło użytkownika
        public void ChangePassword(string newPassword, bool isStudent)
        {
            var userId = _userContextService.GetUserId;

            if (isStudent == true)
            {
                var student = _dbContext.Student.FirstOrDefault(student => student.Id == userId);
                if (student == null)
                {
                    throw new NotFoundException("User not found");
                }
                student.Password = _passwordHasherStudent.HashPassword(student, newPassword);
                _dbContext.SaveChanges();
            }
            else
            {
                var teacher = _dbContext.Teacher.FirstOrDefault(teacher => teacher.Id == userId);
                if (teacher == null)
                {
                    throw new NotFoundException("User not found");
                }
                teacher.Password = _passwordHasherTeacher.HashPassword(teacher, newPassword);
                _dbContext.SaveChanges();

            }
        }

    }
}
