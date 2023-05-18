﻿using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto;

public class StudentReportGetDto
{
    public DateTime SentAt { get; set; }
    public string? Note { get; set; }
    public DateTime LastModified { get; set; }
    public bool ToCheck { get; set; }
    public MarkEnum? Mark { get; set; }
}