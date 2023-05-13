using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto;

public class StudentCollaborateReportGetDto
{
    public int Id { get; set; }
    public string SubgroupName { get; set; }
    public MarkEnum? Mark { get; set; }
}