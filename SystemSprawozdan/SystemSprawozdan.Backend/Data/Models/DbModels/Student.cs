namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Boolean IsDeleted { get; set; } = false;

        public ICollection<SubjectSubgroup> SubjectSubgroups { get; set; }
        public ICollection<ReportComment> ReportComments { get; set; }
    }
}
