﻿using Microsoft.AspNetCore.Http;

namespace SystemSprawozdan.Shared.Dto
{
    public class StudentReportPutDto
    {
        public string? ReportCommentFromStudent { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}