using System.ComponentModel.DataAnnotations;

namespace SystemSprawozdan.Shared.Dto
{
    public class RestoreUserPasswordDto
    {
        [Required(ErrorMessage = "Pole wymagane")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        public string Email { get; set; }
    }
}
