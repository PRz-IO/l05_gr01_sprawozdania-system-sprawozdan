using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Shared.Dto
{
    public class SubmittedStudentReportGetDto
    {   
        
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public string TeacherDegree { get; set; }
        public string ReportTopic { get; set; }
        public DateTime? Deadline { get; set; }
        public MarkEnum? Mark { get; set; }
    }
}

