namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class Major
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartedAt { get; set; }
        public string MajorCode { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
