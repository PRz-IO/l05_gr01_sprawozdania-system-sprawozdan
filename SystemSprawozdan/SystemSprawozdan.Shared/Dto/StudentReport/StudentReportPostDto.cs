using Microsoft.AspNetCore.Http;

namespace SystemSprawozdan.Shared.Dto
{
    public class StudentReportPostDto
    {
        public string? Note { get; set; }
        public int ReportTopicId { get; set; }
        public bool IsIndividual { get; set; }
    }
}
