namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class StudentReportPostDto
    {
        public string? Note { get; set; }
        public int ReportTopicId { get; set; }
        public List<IFormFile>? Files { get; set; }

    }
}
