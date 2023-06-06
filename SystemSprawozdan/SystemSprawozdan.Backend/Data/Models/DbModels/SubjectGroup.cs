namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class SubjectGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupType { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }

        public Subject Subject { get; set; }
        public Teacher Teacher{ get; set; }
        public ICollection<SubjectSubgroup> SubjectSubgroups { get; set; }
        public ICollection<ReportTopic> ReportTopics { get; set; }
    }
}
