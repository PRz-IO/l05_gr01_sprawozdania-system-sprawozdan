namespace SystemSprawozdan.Shared.Dto;

public class SubjectSubgroupBasicGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<StudentBasicGetDto> Students { get; set; }
}