namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class SubjectGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupType { get; set; }
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
        public ICollection<SubjectSubgroup> subjectSubgroups { get; set; }
        public ICollection<ReportTopic> reportTopics { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
