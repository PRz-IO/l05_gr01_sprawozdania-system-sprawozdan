using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto.ReportTopic
{
    public class ReportTopicForStudentGetDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string GroupType { get; set; }
        public TeacherBasicGetDto Teacher { get; set; }
        public string ReportTopicName { get; set; }
        public DateTime? Deadline { get; set; }
        public MarkEnum? Mark { get; set; }
        public int? StudentReportId { get; set; }
        public DateTime? SentAt { get; set; }
        public string StudentName { get; set; }
    }
}
