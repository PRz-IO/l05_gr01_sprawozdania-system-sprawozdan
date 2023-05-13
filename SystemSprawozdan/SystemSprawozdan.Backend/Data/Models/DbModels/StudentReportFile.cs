using System.Reflection.Metadata;

namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class StudentReportFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public string? ContentType { get; set; }
        public int? StudentReportId { get; set; }

        public StudentReport StudentReport { get; set; }
    }
}