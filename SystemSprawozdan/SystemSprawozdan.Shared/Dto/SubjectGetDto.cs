namespace SystemSprawozdan.Backend.Data.Models.Dto
{
    public class SubjectGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MajorCode { get; set; }
        public List<SubjectGroupGetDto> SubjectGroups { get; set; }
    }
}
