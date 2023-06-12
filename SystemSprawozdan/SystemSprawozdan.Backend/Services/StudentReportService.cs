using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportService
    {
        StudentReport PostStudentReport(StudentReportPostDto postStudentReportDto);
        void PutStudentReportAsStudent(int studentReportId, StudentReportAsStudentPutDto studentReportAsStudentPutDto);
        void PutStudentReportAsTeacher(int studentReportId,
            StudentReportAsTeacherPutDto studentReportAsTeacherPutDto);
        StudentReportGetDto GetStudentReport(int studentReportId);
        List<StudentReportGetDto> GetStudentReportsByTopicId(int reportTopicId, bool? isIndividual, bool? toCheck);
        List<StudentBasicGetDto> GetStudentWithoutReportByTopicId(int reportTopicId);
    }

    public class StudentReportService : IStudentReportService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public StudentReportService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public StudentReport PostStudentReport(StudentReportPostDto postStudentReportDto)
        {
            var loginUserId = _userContextService.GetUserId;

            var subjectGroup = _dbContext
                .SubjectGroup.FirstOrDefault(subjectGroup =>
                    subjectGroup.ReportTopics.Any(reportTopic =>
                        reportTopic.Id == postStudentReportDto.ReportTopicId
                    )
                );

            var subjectSubgroup = _dbContext.SubjectSubgroup.FirstOrDefault(subGroup =>
                subGroup.SubjectGroup.Id == subjectGroup.Id &&
                subGroup.Students.Any(student => student.Id == loginUserId) && subGroup.IsIndividual == postStudentReportDto.IsIndividual
            );
            
            if (subjectSubgroup is null)
            {
                throw new BadRequestException($"You don't belong to this subject group!");
            }

            var newStudentReport = new StudentReport()
            {
                SentAt = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                StudentNote = postStudentReportDto.StudentNote,
                ReportTopicId = postStudentReportDto.ReportTopicId,
                SubjectSubgroupId = subjectSubgroup.Id
            };
            _dbContext.StudentReport.Add(newStudentReport);
            _dbContext.SaveChanges();


            var result = newStudentReport;
            return result;
        }

        public void PutStudentReportAsStudent(int studentReportId, StudentReportAsStudentPutDto studentReportAsStudentPutDto)
        {
            var reportToEdit = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);

            if (reportToEdit is null)
            {
                throw new NotFoundException($"You don't have report with id {studentReportId}");
            }
            
            reportToEdit.StudentNote = studentReportAsStudentPutDto.StudentNote;
            reportToEdit.LastModified = DateTime.UtcNow;
            reportToEdit.ToCheck = true;
            
            _dbContext.SaveChanges();
        }

        public void PutStudentReportAsTeacher(int studentReportId, StudentReportAsTeacherPutDto studentReportAsTeacherPutDto)
        {
            var reportToEdit = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);

            if (reportToEdit is null)
            {
                throw new NotFoundException($"You don't have report with id {studentReportId}");
            }
            
            reportToEdit.TeacherNote = studentReportAsTeacherPutDto.TeacherNote;
            reportToEdit.Mark = studentReportAsTeacherPutDto.Mark;
            reportToEdit.ToCheck = false;
            
            _dbContext.SaveChanges();
        }
        
        
        public StudentReportGetDto GetStudentReport(int studentReportId)
        {
            var studentReport = _dbContext.StudentReport
                .Include(report => report.SubjectSubgroup)
                .ThenInclude(subgroup => subgroup.Students)
                .FirstOrDefault(report => report.Id == studentReportId);
            var studentReportDto = _mapper.Map<StudentReportGetDto>(studentReport);

            return studentReportDto;
        }
        
        
        public List<StudentReportGetDto> GetStudentReportsByTopicId(int reportTopicId, bool? isIndividual, bool? toCheck)
        {
            var isReportTopicExist = _dbContext.ReportTopic.Any(reportTopic => reportTopic.Id == reportTopicId);

            if (!isReportTopicExist) throw new NotFoundException($"Report Topic with Id equals {reportTopicId} doesn't exist!");

            var reports = _dbContext.StudentReport
                .Include(report => report.SubjectSubgroup)
                    .ThenInclude(subgroup => subgroup.Students)
                .Where(report => report.ReportTopicId == reportTopicId);

            if (toCheck is not null)
            {
                if (toCheck == true)
                {
                    reports = reports.Where(report => toCheck == report.ToCheck || report.Mark == null);
                }
                else
                {
                    reports = reports.Where(report => toCheck == report.ToCheck && report.Mark != null);
                }
            }
            
            if (isIndividual is not null)
                reports = reports.Where(report => report.SubjectSubgroup.IsIndividual == isIndividual);

            var reportsGetDto = _mapper.Map<List<StudentReportGetDto>>(reports.ToList());
            return reportsGetDto;
        }

        public List<StudentBasicGetDto> GetStudentWithoutReportByTopicId(int reportTopicId)
        {
            var studentsWithoutReport = _dbContext
                .Student
                .Where(student =>
                    student.SubjectSubgroups.Any(subgroup => subgroup.SubjectGroup.ReportTopics.Any(reportTopic => reportTopic.Id == reportTopicId)) &&
                    null == student.SubjectSubgroups.FirstOrDefault(subgroup => 
                        subgroup.SubjectGroup.ReportTopics.Any(reportTopic => 
                            reportTopic.Id == reportTopicId && 
                            reportTopic.StudentReports.Any(studentReport => 
                                studentReport.SubjectSubgroupId == subgroup.Id))))
                .ToList();

            var studentsWithoutReportDto = _mapper.Map<List<StudentBasicGetDto>>(studentsWithoutReport);
            return studentsWithoutReportDto;
        }
    }
}

            
