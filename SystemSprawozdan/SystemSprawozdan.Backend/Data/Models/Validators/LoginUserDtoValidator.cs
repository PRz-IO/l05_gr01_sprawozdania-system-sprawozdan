using FluentValidation;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Data.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(user => user.Password)
                .MinimumLength(8);
        }
    }
}
