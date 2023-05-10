namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class GroupTypeSubjectSubgroupDto
    {
        public string? Name { get; set; }  // is that column really needed?
        public int Id { get; set; }
        public int SubjectGroupId { get; set; }

    }
}
