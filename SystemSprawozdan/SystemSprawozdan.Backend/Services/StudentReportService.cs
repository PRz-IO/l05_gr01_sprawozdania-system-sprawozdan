using System.Security.Claims;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportService
    {
        void SendStudentReport(SendStudentReportDto sendStudentReportDto, int reportTopicId, int subjectGroupId);
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

        public void SendStudentReport(SendStudentReportDto sendStudentReportDto,int reportTopicId, int subjectGroupId)
        {
            //var subjectSubGroup = _dbContext.SubjectSubgroup. FirstOrDefault(subjectSubGroup => subjectSubGroup.Students.Equals(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value)

            var newStudentReport = new StudentReport()
            {   
                SentAt = DateTime.UtcNow,
                Note = sendStudentReportDto.Note,
                ReportTopicId = reportTopicId,
                SubjectSubgroupId = sendStudentReportDto.SubjectSubgroupId,
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
