namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string Position { get; set; }
        public Boolean IsDeleted { get; set; } = false;

        public ICollection<ReportComment> ReportComments { get; set; }
        public ICollection<SubjectGroup> SubjectGroups { get; set; }
    }
}
