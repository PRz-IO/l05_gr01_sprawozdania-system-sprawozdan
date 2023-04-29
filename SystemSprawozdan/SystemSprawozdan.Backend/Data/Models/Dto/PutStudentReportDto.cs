namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class PutStudentReportDto
    {
        public string? ReportCommentFromStudent { get; set; }
        public IFormFile? OptionalFile { get; set; }
    }
}
