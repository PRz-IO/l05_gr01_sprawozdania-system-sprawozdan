using Microsoft.AspNetCore.Http;

namespace SystemSprawozdan.Shared.Dto
{
    public class StudentReportPostDto
    {
        public string? Note { get; set; }
        public string ReportTopicId { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string IsIndividual { get; set; }
    }
}