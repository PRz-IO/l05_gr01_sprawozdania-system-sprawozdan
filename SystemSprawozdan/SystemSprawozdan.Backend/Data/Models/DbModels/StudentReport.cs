using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class StudentReport
    {
        public int Id { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? LastModified { get; set; }
        public string? Note { get; set; }
        public int ReportTopicId { get; set; }
        public int SubjectSubgroupId { get; set; }
        public bool ToCheck { get; set; } = true;
        public MarkEnum? Mark { get; set; }

        public SubjectSubgroup SubjectSubgroup { get; set; }
        public ReportTopic ReportTopic { get; set; }
        public ICollection<ReportComment> ReportComments { get; set; }
        public ICollection<StudentReportFile> studentReportFiles { get; set; }
    }
}
