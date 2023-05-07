using System.ComponentModel.DataAnnotations;

namespace SystemSprawozdan.Shared.Dto
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        public string Password { get; set; }
    }
}
