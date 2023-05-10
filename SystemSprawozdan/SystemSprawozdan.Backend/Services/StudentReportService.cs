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
        void PostStudentReport(StudentReportPostDto postStudentReportDto);
        void PutStudentReport(int studentReportId, StudentReportPutDto putStudentReportDto);
        IEnumerable<ReportTopicDto> GetReports(bool? toCheck);
    }

    public class StudentReportService : IStudentReportService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public StudentReportService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public void PostStudentReport(StudentReportPostDto postStudentReportDto)
        {
            var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            

            var subjectGroup = _dbContext
                .SubjectGroup.FirstOrDefault(subjectGroup =>
                    subjectGroup.reportTopics.Any(reportTopic =>
                        reportTopic.Id == postStudentReportDto.ReportTopicId
                    )
                );

            var subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subjectSubgroup =>
                 subjectSubgroup.SubjectGroup.Id == subjectGroup.Id &&
                 subjectSubgroup.Students.Any(student => student.Id == loginUserId)
            );
            
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
                ReportTopicId = postStudentReportDto.ReportTopicId,
                SubjectSubgroupId = subjectSubgroup.Id
                
            };
            _dbContext.StudentReport.Add(newStudentReport);
            _dbContext.SaveChanges();

            if (postStudentReportDto.Files is null) return;
            {
                foreach (FormFile file in postStudentReportDto.Files)
                {
                    if (file is not null)
                    {
                        if (file.Length > 0)
                        {
                            using var memoryStream = new MemoryStream();
                            file.CopyToAsync(memoryStream);
                            var studentReportFile = new StudentReportFile()
                            {
                                StudentReportId = newStudentReport.Id,
                                File = memoryStream.ToArray()
                            };

                            _dbContext.StudentReportFile.Add(studentReportFile);
                        }
                    }
                }
            }
            _dbContext.SaveChanges();
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
            _dbContext.SaveChanges();

            if (putStudentReportDto.Files is null) return;

            foreach (FormFile file in putStudentReportDto.Files)
            {
                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        file.CopyToAsync(memoryStream);
                        var studentReportFile = new StudentReportFile()
                        {
                            StudentReportId = reportToEdit.Id,
                            File = memoryStream.ToArray()
                        };

                        _dbContext.StudentReportFile.Add(studentReportFile);
                    }
                }
            }
            reportToEdit.LastModified = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

        public IEnumerable<ReportTopicDto> GetReports(bool? toCheck)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                null,
                new TeacherResourceOperationRequirement(TeacherResourceOperation.Read)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var teacherId = _userContextService.GetUserId;
            
            var reportsFromDb = _dbContext.ReportTopic
                        .Include(reportTopic => reportTopic.SubjectGroup)
                            .ThenInclude(subjectGroup => subjectGroup.Subject)
                                .ThenInclude(subject => subject.Major)  
                        .Where(reportTopic => reportTopic.SubjectGroup.Teachers.Any(teacher => teacher.Id == teacherId));

            if (toCheck != null)
            {
                reportsFromDb = reportsFromDb.Where(reportTopic =>
                    reportTopic.StudentReports.Any(studentReport => studentReport.ToCheck == toCheck));
            }
            
            var reportsFromDbList = reportsFromDb.OrderBy(reportTopic => reportTopic.Deadline).ToList();
            
            var reportsDto = _mapper.Map<List<ReportTopicDto>>(reportsFromDbList);
            
            return reportsDto;
        }
    }
}

            

