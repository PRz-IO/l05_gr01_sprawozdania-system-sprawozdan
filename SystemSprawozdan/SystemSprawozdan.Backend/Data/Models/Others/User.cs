using SystemSprawozdan.Backend.Data.Enums;

namespace SystemSprawozdan.Backend.Data.Models.Others
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRoleEnum UserRole { get; set; } 
    }
}
