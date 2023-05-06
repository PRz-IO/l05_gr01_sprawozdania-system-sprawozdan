using Microsoft.AspNetCore.Http;

namespace SystemSprawozdan.Shared.Dto
{
    public class PostStudentReportDto
    {
        public string? Note { get; set; }
        public int ReportTopicId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
