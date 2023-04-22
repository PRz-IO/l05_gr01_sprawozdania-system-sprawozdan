using FluentValidation;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Data.Models.Validators
{
    public class RegisterStudentDtoValidator : AbstractValidator<RegisterStudentDto>
    {
        public RegisterStudentDtoValidator(ApiDbContext apiDbContext)
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((email, context) =>
                {
                    var emailInUse = apiDbContext.Student.Any(user => user.Email == email)
                                        || apiDbContext.Teacher.Any(user => user.Email == email);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });

            RuleFor(user => user.Login)
                .NotEmpty()
                .Custom((login, context) =>
                {
                    var loginInUse = apiDbContext.Student.Any(user => user.IsDeleted == false && user.Login == login)
                                        || apiDbContext.Teacher.Any(user => user.IsDeleted == false && user.Login == login);

                    if(loginInUse)
                    {
                        context.AddFailure("Login", "That login is taken");
                    }
                });

            RuleFor(user => user.Password)
                .MinimumLength(8)
                .Equal(user => user.ConfirmPassword);
        }
    }
}
