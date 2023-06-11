namespace SystemSprawozdan.Shared.Dto
{
    public class ReportTopicGetDto
    {
        public int Id { get; set; }
        public string? SubjectName { get; set; }
        public string ReportTopicName { get; set; }
        public string? MajorName { get; set; }
        public string? SubjectGroupName { get; set; }
        public string? SubjectGroupType{ get; set; }
        public DateTime ReportTopicDeadline { get; set; }
    }
}
