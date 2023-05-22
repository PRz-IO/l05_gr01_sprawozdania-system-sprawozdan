using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto;

public class StudentReportGetDto
{
    public int Id { get; set; }
    public SubjectSubgroupBasicGetDto SubjectSubgroup { get; set; }
    public MarkEnum? Mark { get; set; }
    public bool IsIndividual { get; set; }
}