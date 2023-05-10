namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class SubjectGroupGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GroupType { get; set; }
        public List<GetTeacherDto> Teachers { get; set; }
        public int SubjectId { get; set; }
        public List<GetSubgroupsDto> Subgroups { get; set; }    
    }
}
