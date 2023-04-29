namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class EditStudentReportDto
    {
        public string? ReportCommentFromStudent { get; set; }
        public IFormFile? OptionalFile { get; set; }
    }
}
