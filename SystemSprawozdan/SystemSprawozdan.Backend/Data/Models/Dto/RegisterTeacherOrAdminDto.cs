using SystemSprawozdan.Backend.Data.Enums;

namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class RegisterTeacherOrAdminDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string Position { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public UserRoleEnum UserRole { get; set; }
    }
}
