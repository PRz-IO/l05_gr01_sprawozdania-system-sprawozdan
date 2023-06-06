using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //TODO: Mikolaj: Logowanie
        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var token = _accountService.LoginUser(loginUserDto);
            return Ok(token);
        }

        //TODO: Mikolaj: Rejestracja
        [HttpPost("register")]
        public ActionResult RegisterStudent([FromBody] RegisterStudentDto registerUserDto)
        {
            _accountService.RegisterStudent(registerUserDto);
            return Ok();
        }

        [HttpPost("create")]
        [Authorize(Roles = nameof(UserRoleEnum.Admin))]
        public ActionResult RegisterTeacherOrAdmin([FromBody] RegisterTeacherOrAdminDto registerTeacherOrAdminDto)
        {
            _accountService.RegisterTeacherOrAdmin(registerTeacherOrAdminDto);
            return Ok();
        }

        [HttpPut("restorePassword")]
        public ActionResult RestoreUserPasswordDto([FromBody] RestoreUserPasswordDto restoreUserPasswordDto)
        {
            _accountService.RestoreUserPassword(restoreUserPasswordDto);
            return Ok();
        }

        //TODO: Bartek: Trzeba zrobic GETa, który wyświetli podstawowe informacje o uzytkowniku
        [HttpGet("studentCredentials")]
        public ActionResult GetUserInfo()
        {
            var zmienna = _accountService.GetUserInfo();
            return Ok(zmienna); 
        }
        //TODO: Bartek: Trzeba zrobić PUTa, którym będzie mozna zresetowac haslo
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword([FromBody] string newPassword)
        { 
            return Ok();
        }
    }
}
