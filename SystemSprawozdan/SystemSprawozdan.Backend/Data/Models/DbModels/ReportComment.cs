﻿namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class ReportComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int StudentReportId { get; set; }
        public int? StudentId { get; set; }
        public int? TeacherId { get; set; }

        public Student Student { get; set; }
        public StudentReport StudentReport { get; set; }
        public Teacher Teacher { get; set; }
    }
}
