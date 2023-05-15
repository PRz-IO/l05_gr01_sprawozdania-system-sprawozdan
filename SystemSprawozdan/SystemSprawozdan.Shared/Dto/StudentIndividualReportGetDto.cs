using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto;

public class StudentIndividualReportGetDto
{
    public int Id { get; set; }
    public StudentBasicGetDto Student { get; set; }
    public MarkEnum? Mark { get; set; }
}