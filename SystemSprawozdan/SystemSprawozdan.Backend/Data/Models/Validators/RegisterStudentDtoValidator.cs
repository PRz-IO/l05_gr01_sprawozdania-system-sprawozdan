using FluentValidation;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Data.Models.Validators
{
    public class RegisterStudentDtoValidator : AbstractValidator<RegisterStudentDto>
    {
        public RegisterStudentDtoValidator(ApiDbContext apiDbContext)
        {
            RuleFor(user => user.Email)
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
                .Custom((login, context) =>
                {
                    var loginInUse = apiDbContext.Student.Any(user => user.IsDeleted == false && user.Login == login)
                                        || apiDbContext.Teacher.Any(user => user.IsDeleted == false && user.Login == login);

                    if(loginInUse)
                    {
                        context.AddFailure("Login", "That login is taken");
                    }
                });
        }
    }
}
