using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportService
    {
        StudentReport PostStudentReport(StudentReportPostDto postStudentReportDto);
        void PutStudentReport(int studentReportId, StudentReportPutDto putStudentReportDto);
        IEnumerable<ReportTopicGetDto> GetReports(bool? toCheck);
        List<StudentReportGetDto> GetStudentReportsByTopicId(int reportTopicId, bool? isIndividual, bool? isMarked);
    }

    public class StudentReportService : IStudentReportService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public StudentReportService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IAuthorizationService authorizationService, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _env = env;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public StudentReport PostStudentReport(StudentReportPostDto postStudentReportDto)
        {
            var loginUserId = _userContextService.GetUserId;
            var reportTopicIdInteger = (postStudentReportDto.ReportTopicId);
            var isIndividualBoolean = (postStudentReportDto.IsIndividual);

            var subjectGroup = _dbContext
                .SubjectGroup.FirstOrDefault(subjectGroup =>
                    subjectGroup.reportTopics.Any(reportTopic =>
                        reportTopic.Id == reportTopicIdInteger
                    )
                );

            var subjectSubgroup = new SubjectSubgroup();
            
            if (isIndividualBoolean == false) 
            {
                subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subGroup =>
                    subGroup.SubjectGroupId == subjectGroup.Id &&
                    subGroup.Students.Any(student => student.Id == loginUserId) && subGroup.IsIndividual == false
                );

                if (subjectSubgroup is null)
                {
                    subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subGroup =>
                        subGroup.SubjectGroupId == subjectGroup.Id &&
                        subGroup.Students.Any(student => student.Id == loginUserId) && subGroup.IsIndividual == true
                    );    
                }
            }

            if (isIndividualBoolean == true) 
            {
                subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subGroup =>
                    subGroup.SubjectGroup.Id == subjectGroup.Id &&
                    subGroup.Students.Any(student => student.Id == loginUserId) && subGroup.IsIndividual == true
                );
            }

            string? noteToSend;
            if (postStudentReportDto.Note != null)
            {
                noteToSend = DateTime.Now.ToString() + ":\n" + postStudentReportDto.Note;
            }
            else
            {
                noteToSend = null;
            }

            var newStudentReport = new StudentReport()
            {
                SentAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Note = noteToSend,
                ReportTopicId = reportTopicIdInteger,
                SubjectSubgroupId = subjectSubgroup.Id
            };
            _dbContext.StudentReport.Add(newStudentReport);
            _dbContext.SaveChanges();


            var result = newStudentReport;
            return result;
        }

        public void PutStudentReport(int studentReportId, StudentReportPutDto putStudentReportDto)
        {
            var reportToEdit = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);
            string previousComment = reportToEdit.Note;
            string commentToInsert;
            var currentDateTime = DateTime.Now.ToString();
            
            if(putStudentReportDto.ReportCommentFromStudent != null)
            {
                if (previousComment != null)
                {
                    commentToInsert = previousComment + "\n\n" + currentDateTime + ":\n" + putStudentReportDto.ReportCommentFromStudent;
                    reportToEdit.Note = commentToInsert;

                }
                else
                {
                    commentToInsert = currentDateTime + ":\n" + putStudentReportDto.ReportCommentFromStudent;
                    reportToEdit.Note = commentToInsert;
                }
            }
            reportToEdit.LastModified = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }
        

        public List<StudentReportGetDto> GetStudentReportsByTopicId(int reportTopicId, bool? isIndividual, bool? isMarked)
        {
            
            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                null,
                new TeacherResourceOperationRequirement(TeacherResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();
            
            var isReportTopicExist = _dbContext.ReportTopic.Any(reportTopic => reportTopic.Id == reportTopicId);

            if (!isReportTopicExist) throw new NotFoundException($"Report Topic with Id equals {reportTopicId} doesn't exist!");

            var reports = _dbContext.StudentReport
                .Include(report => report.SubjectSubgroup)
                    .ThenInclude(subgroup => subgroup.Students)
                .Where(report => report.ReportTopicId == reportTopicId);

            if (isMarked is not null)
                reports = reports.Where(report => isMarked == (report.Mark != null));

            if (isIndividual is not null)
                reports = reports.Where(report => report.SubjectSubgroup.IsIndividual == isIndividual);

            var reportsGetDto = _mapper.Map<List<StudentReportGetDto>>(reports.ToList());
            return reportsGetDto;
        }
    }
}

            
