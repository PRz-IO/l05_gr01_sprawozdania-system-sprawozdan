using Microsoft.AspNetCore.Http;

namespace SystemSprawozdan.Shared.Dto
{
    public class PutStudentReportDto
    {
        public string? ReportCommentFromStudent { get; set; }
        public List<IFormFile>? OptionalFiles { get; set; }
    }
}
