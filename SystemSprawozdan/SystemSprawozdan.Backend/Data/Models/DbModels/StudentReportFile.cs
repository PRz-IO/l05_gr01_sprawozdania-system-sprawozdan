using System.Reflection.Metadata;

namespace SystemSprawozdan.Backend.Data.Models.DbModels
{
    public class StudentReportFile
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        public int StudentReportId { get; set; }

        public StudentReport StudentReport { get; set; }
    }
}
