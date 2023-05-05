using System.ComponentModel.DataAnnotations;

namespace SystemSprawozdan.Shared.Dto
{
    public class LoginUserDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
