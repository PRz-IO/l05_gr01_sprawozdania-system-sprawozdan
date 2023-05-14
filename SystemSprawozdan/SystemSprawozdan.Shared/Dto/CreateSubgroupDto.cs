namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class CreateSubgroupDto
    {
        public int SubjectGroupId { get; set; }
        public bool isIndividual { get; set; }
        public string? SubgroupName { get; set; }
    }
}
