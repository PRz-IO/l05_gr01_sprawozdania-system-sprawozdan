namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class Term
    {
        public int Id { get; set; }
        public int TermNumber { get; set; }
        public DateOnly StartedAt { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
