namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class SendStudentReportDto
    {
        public string? Note { get; set; }
        public int ReportTopicId { get; set; }
        public IFormFile File { get; set; }

    }
}
