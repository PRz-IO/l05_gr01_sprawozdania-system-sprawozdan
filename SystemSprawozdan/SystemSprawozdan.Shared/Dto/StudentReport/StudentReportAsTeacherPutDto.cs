using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto;

public class StudentReportAsTeacherPutDto
{
    public MarkEnum? Mark { get; set; }
    public string? TeacherNote { get; set; }
}