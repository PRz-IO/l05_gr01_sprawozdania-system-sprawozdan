using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Exceptions;
using System.Text.RegularExpressions;
using SystemSprawozdan.Backend.Data.Models.DbModels;

namespace SystemSprawozdan.Backend.Services
{
    public interface IReportTopicService
    {
        ReportTopicGetDto GetReportTopic(int? reportTopicId, int? studentReportId);
        IEnumerable<ReportTopicGetDto> GetReports(bool? toCheck);
        ReportTopicGetDto GetReportById(int reportTopicId);
        List<ReportTopicGetDto> GetReportTopicForGroup(int groupId);
        void PostReportTopic(ReportTopicPostDto reportTopic);
    }

    public class ReportTopicService : IReportTopicService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public ReportTopicService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
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
            var teacherId = _userContextService.GetUserId;

            var reportsFromDb = _dbContext.ReportTopic
                .Include(reportTopic => reportTopic.SubjectGroup)
                .ThenInclude(subjectGroup => subjectGroup.Subject)
                .ThenInclude(subject => subject.Major)  
                .Where(reportTopic => reportTopic.SubjectGroup.TeacherId == teacherId);
            
            if (toCheck == true)
                reportsFromDb = reportsFromDb.Where(reportTopic =>
                    reportTopic.StudentReports.Any(studentReport => studentReport.ToCheck == true)
                    || reportTopic.StudentReports.Count == 0);
            
            else if(toCheck == false)
                reportsFromDb = reportsFromDb.Where(reportTopic =>
                    !(reportTopic.StudentReports.Any(studentReport => studentReport.ToCheck == true)
                    || reportTopic.StudentReports.Count == 0));
            
            var reportsFromDbList = reportsFromDb.OrderBy(reportTopic => reportTopic.Deadline).ToList();
            
            var reportsDto = _mapper.Map<List<ReportTopicGetDto>>(reportsFromDbList);
            
            return reportsDto;
        }
        
        
        public ReportTopicGetDto GetReportById(int reportTopicId)
        {
            var teacherId = _userContextService.GetUserId;

            var reportFromDb = _dbContext.ReportTopic
                .Include(reportTopic => reportTopic.SubjectGroup)
                .ThenInclude(subjectGroup => subjectGroup.Subject)
                .ThenInclude(subject => subject.Major)  
                .FirstOrDefault(reportTopic => reportTopic.Id == reportTopicId);

            if (reportFromDb is null)
                throw new NotFoundException($"Not found report topic with Id equals {reportTopicId}!");
            
            var reportDto = _mapper.Map<ReportTopicGetDto>(reportFromDb);
            
            return reportDto;
        }
        public List<ReportTopicGetDto> GetReportTopicForGroup(int groupId)
        {
            List<ReportTopicGetDto> reportTopics = new List<ReportTopicGetDto>();

            var topics = _dbContext.ReportTopic.Where(topic => topic.SubjectGroupId == groupId).ToList();

            foreach ( var topic in topics)
            {
                reportTopics.Add(new ReportTopicGetDto
                {
                    Id = topic.Id,
                    ReportTopicName = topic.Name,
                    ReportTopicDeadline = topic.Deadline
                });
            }

            return reportTopics;
        }
        public void PostReportTopic(ReportTopicPostDto reportTopic)
        {
            var newReportTopic = new ReportTopic()
            {
                Name = reportTopic.Name,
                Deadline = reportTopic.Deadline,
                SubjectGroupId = reportTopic.GroupId
            };
            _dbContext.ReportTopic.Add(newReportTopic);
            _dbContext.SaveChanges();
        }
    }
}
