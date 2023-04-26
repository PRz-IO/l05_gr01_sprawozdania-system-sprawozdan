using System.Security.Claims;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportService
    {
        void SendStudentReport(SendStudentReportDto sendStudentReportDto);
        void EditStudentReport(int studentReportId);
    }

    public class StudentReportService : IStudentReportService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public StudentReportService(ApiDbContext dbContext, IUserContextService userContextService )
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public void SendStudentReport(SendStudentReportDto sendStudentReportDto)
        {
            var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var subjectGroup = _dbContext
                .SubjectGroup.FirstOrDefault(subjectGroup =>
                    subjectGroup.reportTopics.Any(reportTopic =>
                        reportTopic.Id == sendStudentReportDto.reportTopicId
                    )
                );

            var subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subjectSubgroup =>
                subjectSubgroup.SubjectGroup.Id == subjectGroup.Id && 
                subjectSubgroup.Students.Any(student => student.Id == loginUserId)
            );

            var newStudentReport = new StudentReport()
            {   
                SentAt = DateTime.UtcNow,
                Note = sendStudentReportDto.Note,
                ReportTopicId = sendStudentReportDto.reportTopicId
                // TODO
                /*
                studentReportFiles = new List<StudentReportFile>
                {
                    new StudentReportFile()
                    {
                        File = sendStudentReportDto.File
                    }
                }
                */
            };

            _dbContext.StudentReport.Add(newStudentReport);
            _dbContext.SaveChanges();
        }


        public void EditStudentReport(int studentReportId)
        {
            var reportToEdit = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);
            reportToEdit.SentAt = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

    }
}
