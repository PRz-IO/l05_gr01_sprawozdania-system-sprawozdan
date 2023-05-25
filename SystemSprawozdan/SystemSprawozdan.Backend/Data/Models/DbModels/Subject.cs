namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MajorId { get; set; }
        public int Term { get; set; }

        public Major Major { get; set; }
        public ICollection<SubjectGroup> SubjectGroups { get; set; }
    }
}
