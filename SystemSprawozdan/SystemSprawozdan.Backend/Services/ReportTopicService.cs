using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Dto.ReportTopic;

namespace SystemSprawozdan.Backend.Services
{
    public interface IReportTopicService
    {
        IEnumerable<ReportTopicGetDto> GetReports(bool? toCheck);

        ReportTopicGetDto GetReportById(int reportTopicId);

        List<ReportTopicForStudentGetDto> GetReportTopicForStudent(bool isSubmitted);



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

        public List<ReportTopicForStudentGetDto> GetReportTopicsByUserId()
        {
            var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var reportTopics = _dbContext.ReportTopic
                    .Where(rt => rt.SubjectGroup.subjectSubgroups
                        .Any(ss => ss.Students.Any(s => s.Id == loginUserId)))
                    .Select(rt => new ReportTopicForStudentGetDto
                    {
                        Id = rt.Id,
                        ReportTopicName = rt.Name,
                        Deadline = rt.Deadline,
                        Teacher = new TeacherBasicGetDto
                        {
                            Degree = rt.SubjectGroup.Teacher.Degree,
                            Name = rt.SubjectGroup.Teacher.Name,
                            Surname = rt.SubjectGroup.Teacher.Surname
                        },
                        SubjectName = rt.SubjectGroup.Subject.Name,
                        GroupType = rt.SubjectGroup.GroupType
                    })
                    .ToList();

            return reportTopics;
            }


            public List<ReportTopicForStudentGetDto> GetSubmittedReportsByStudentId()
            {
            var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            //var submittedReports = _context.StudentReport
            //    .Where(r => r.SubjectSubgroup.Students.Any(s => s.Id == loginUserId))
            //    .OrderByDescending(r => r.SentAt)
            //    .ToList();

            var subjectSubgroups = _dbContext.SubjectSubgroup
            .Where(sg => sg.Students.Any(s => s.Id == loginUserId))
            .ToList();

            var submittedReportsDto = new List<ReportTopicForStudentGetDto>();

            foreach (var subjectSubgroup in subjectSubgroups)
            {

                var submittedReports = _dbContext.StudentReport
                .Where(r => r.SubjectSubgroupId == subjectSubgroup.Id)
                .OrderByDescending(r => r.SentAt)
                .ToList();

                foreach (var report in submittedReports)
                {
                    var subjectGroup = _dbContext.SubjectGroup
                        .FirstOrDefault(g => g.Id == report.SubjectSubgroupId);

                    var subject = _dbContext.Subject
                        .FirstOrDefault(s => s.Id == subjectGroup.SubjectId);

                    var teacher = _dbContext.Teacher
                        .FirstOrDefault(t => t.SubjectGroups.Contains(subjectGroup));

                    var reportTopic = _dbContext.ReportTopic
                        .FirstOrDefault(rt => rt.Id == report.ReportTopicId);

                    var submittedReportDto = new ReportTopicForStudentGetDto
                    {
                        SubjectName = subject.Name,
                        GroupType = subjectGroup.GroupType,
                        Teacher = new TeacherBasicGetDto
                        {
                            Degree = teacher.Degree,
                            Name = teacher.Name,
                            Surname = teacher.Surname
                        },
                        ReportTopicName = reportTopic.Name,
                        Deadline = reportTopic.Deadline,
                        Mark = report.Mark
                    };



                    submittedReportsDto.Add(submittedReportDto);
                }
            }
            return submittedReportsDto;
        }

        public List<ReportTopicForStudentGetDto> GetReportTopicForStudent(bool isSubmitted)
        {
            List<ReportTopicForStudentGetDto> report = new List<ReportTopicForStudentGetDto>();
            if (isSubmitted)
            {
                report = GetSubmittedReportsByStudentId();
            }
            else
            {
                report = GetReportTopicsByUserId();
            }
            return report;
        }

    }
}
