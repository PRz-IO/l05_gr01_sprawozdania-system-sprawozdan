namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class ReportTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public int SubjectGroupId { get; set; }

        public SubjectGroup SubjectGroup { get; set; }
        public ICollection<StudentReport> StudentReports { get; set; }
    }
}
