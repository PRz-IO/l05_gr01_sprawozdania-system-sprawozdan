using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface IReportTopicService
    {
        ReportTopicGetDto GetReportTopic(int? reportTopicId, int? studentReportId);
        IEnumerable<ReportTopicGetDto> GetReports(bool? toCheck);
        ReportTopicGetDto GetReportById(int reportTopicId);
    }

    public class ReportTopicService : IReportTopicService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public ReportTopicService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public ReportTopicGetDto GetReportTopic(int? reportTopicId, int? studentReportId)
        {
            var reportTopicDto = new ReportTopicGetDto();
            if (reportTopicId != null)
            {
                var reportTopic = _dbContext.ReportTopic.FirstOrDefault(t => t.Id == reportTopicId);
                if (reportTopic is null) throw new BadRequestException($"Nie ma takiego tematu sprawozdania z ID = {reportTopicId}!");
                reportTopicDto.ReportTopicName = reportTopic.Name;
                reportTopicDto.ReportTopicDeadline = reportTopic.Deadline; 
                
            }
            

            if (studentReportId != null)
            {
                var studentReport = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);
                var reportTopic = _dbContext.ReportTopic.FirstOrDefault(topic => topic.Id == studentReport.ReportTopicId);

                reportTopicDto.ReportTopicName = reportTopic.Name;
                reportTopicDto.ReportTopicDeadline = reportTopic.Deadline;
            }

            return reportTopicDto;
        }
        
        
        public IEnumerable<ReportTopicGetDto> GetReports(bool? toCheck)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                null,
                new TeacherResourceOperationRequirement(TeacherResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            var teacherId = _userContextService.GetUserId;

            var reportsFromDb = _dbContext.ReportTopic
                .Include(reportTopic => reportTopic.SubjectGroup)
                .ThenInclude(subjectGroup => subjectGroup.Subject)
                .ThenInclude(subject => subject.Major)  
                .Where(reportTopic => reportTopic.SubjectGroup.TeacherId == teacherId);

            if (toCheck != null)
            {
                reportsFromDb = reportsFromDb.Where(reportTopic =>
                    reportTopic.StudentReports.Any(studentReport => studentReport.ToCheck == toCheck));
            }
            
            var reportsFromDbList = reportsFromDb.OrderBy(reportTopic => reportTopic.Deadline).ToList();
            
            var reportsDto = _mapper.Map<List<ReportTopicGetDto>>(reportsFromDbList);
            
            return reportsDto;
        }
        
        
        public ReportTopicGetDto GetReportById(int reportTopicId)
        {
            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                null,
                new TeacherResourceOperationRequirement(TeacherResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
                throw new ForbidException();

            var teacherId = _userContextService.GetUserId;

            var reportFromDb = _dbContext.ReportTopic
                .Include(reportTopic => reportTopic.SubjectGroup)
                .ThenInclude(subjectGroup => subjectGroup.Subject)
                .ThenInclude(subject => subject.Major)  
                .FirstOrDefault(reportTopic => reportTopic.SubjectGroup.TeacherId == teacherId && reportTopic.Id == reportTopicId);

            if (reportFromDb is null)
                throw new NotFoundException($"Not found report topic with Id equals {reportTopicId}!");
            
            var reportDto = _mapper.Map<ReportTopicGetDto>(reportFromDb);
            
            return reportDto;
        }
    }
}
