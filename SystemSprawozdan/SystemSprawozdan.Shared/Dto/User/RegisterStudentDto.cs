using System.ComponentModel.DataAnnotations;

namespace SystemSprawozdan.Shared.Dto
{
    public class RegisterStudentDto
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]+$", ErrorMessage = "Dozwolone tylko litery")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^[a-zA-ZąćęłńóśźżĄĘŁŃÓŚŹŻ]+$", ErrorMessage = "Dozwolone tylko litery")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Dozwolone tylko cyfry")]
        [MaxLength(6, ErrorMessage = "Numer albumu składa się z 6 znaków")]
        [MinLength(6, ErrorMessage = "Numer albumu składa się z 6 znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Podaj hasło")]
        [MinLength(8, ErrorMessage = "Minimalna długość hasła to 8")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Podaj hasło")]
        [Compare("Password", ErrorMessage = "Hasła są różne")]
        public string ConfirmPassword { get; set; }
    }
}
