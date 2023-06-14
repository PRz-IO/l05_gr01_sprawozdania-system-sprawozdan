using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Exceptions;
using System.Text.RegularExpressions;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto.ReportTopic;

namespace SystemSprawozdan.Backend.Services
{
    public interface IReportTopicService
    {
        ReportTopicGetDto GetReportTopic(int? reportTopicId, int? studentReportId);
        IEnumerable<ReportTopicGetDto> GetReports(bool? toCheck);
        ReportTopicGetDto GetReportById(int reportTopicId);
        List<ReportTopicGetDto> GetReportTopicForGroup(int groupId);
        void PostReportTopic(ReportTopicPostDto reportTopic);
        List<ReportTopicForStudentGetDto> GetReportTopicForStudent(bool isSubmitted);
        List<ReportTopicForStudentGetDto> GetSubmittedReportsByStudentAndSubject(int studentId, int subjectId);
 


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
        //! Zwraca tematy sprawozda� dla danej grupy
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
        //! Dodaje temat sprawozdania dla danej grupy
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
        
        private List<ReportTopicForStudentGetDto> GetReportTopicsByUserId()
        {
            var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var reportTopics = _dbContext.ReportTopic
                    .Where(rt => rt.SubjectGroup.SubjectSubgroups
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


        private List<ReportTopicForStudentGetDto> GetSubmittedReportsByStudentId()
        { var loginUserId = int.Parse(_userContextService.User.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

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
                    var subjectSubGroup = _dbContext.SubjectSubgroup
                       .FirstOrDefault(g => g.Id == report.SubjectSubgroupId);

                    var subjectGroup = _dbContext.SubjectGroup
                        .FirstOrDefault(g => g.Id == subjectSubGroup.SubjectGroupId);

                    var subject = _dbContext.Subject
                        .FirstOrDefault(s => s.Id == subjectGroup.SubjectId);

                    var teacher = _dbContext.Teacher
                        .FirstOrDefault(t => t.SubjectGroups.Contains(subjectGroup));

                    var reportTopic = _dbContext.ReportTopic
                        .FirstOrDefault(rt => rt.Id == report.ReportTopicId);

                    var submittedReportDto = new ReportTopicForStudentGetDto
                    {
                        StudentReportId = report.Id,
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
        
        public List<ReportTopicForStudentGetDto> GetSubmittedReportsByStudentAndSubject(int studentId, int subjectId)
        {
            var subjectSubgroups = _dbContext.SubjectSubgroup
            .Where(sg => sg.Students.Any(s => s.Id == studentId))
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
                    .FirstOrDefault(s => s.Id == subjectGroup.SubjectId && s.Id == subjectId);

                var teacher = _dbContext.Teacher
                    .FirstOrDefault(t => t.SubjectGroups.Contains(subjectGroup));

                var reportTopic = _dbContext.ReportTopic
                    .FirstOrDefault(rt => rt.Id == report.ReportTopicId);

                var studentName = GetStudentName(studentId);

                var submittedReportDto = new ReportTopicForStudentGetDto
                {
                    StudentReportId = report.Id,
                    SubjectName = subject.Name,
                    GroupType = subjectGroup.GroupType,
                    Teacher = new TeacherBasicGetDto
                    {
                        Degree = teacher.Degree,
                        Name = teacher.Name,
                        Surname = teacher.Surname
                    },
                    ReportTopicName = reportTopic.Name,
                    SentAt = report.SentAt,
                    Mark = report.Mark,
                    StudentName = GetStudentName(studentId)
                };
                submittedReportsDto.Add(submittedReportDto);
            }
            }
            
            return submittedReportsDto;
        }

        private string GetStudentName(int studentId)
        {
            var student = _dbContext.Student.FirstOrDefault(s => s.Id == studentId);
            return $"{student?.Name} {student?.Surname}";
        }
    }
}
