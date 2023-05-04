namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class StudentReportPutDto
    {
        public string? ReportCommentFromStudent { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
