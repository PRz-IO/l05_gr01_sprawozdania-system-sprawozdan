namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class SubjectSubgroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SubjectGroup SubjectGroup { get; set; }
    }
}
