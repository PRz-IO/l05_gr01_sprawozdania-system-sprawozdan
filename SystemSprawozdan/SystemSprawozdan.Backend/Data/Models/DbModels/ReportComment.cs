namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class ReportComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int? UserId { get; set; }
        public int? StudentReportId { get; set; }

        public Student Student { get; set; }
        public StudentReport StudentReport { get; set; }
        public Teacher Teacher { get; set; }
    }
}
