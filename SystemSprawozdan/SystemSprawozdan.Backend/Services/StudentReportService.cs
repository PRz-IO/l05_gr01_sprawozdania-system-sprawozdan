using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportService
    {
        void PostStudentReport(StudentReportPostDto postStudentReportDto);
        void PutStudentReport(int studentReportId, StudentReportPutDto putStudentReportDto);
        Task<List<StudentReportFile>> UploadFile(int? studentReportId, List<IFormFile> files);
        IEnumerable<ReportTopicDto> GetReports(bool? toCheck);
    }

    public class StudentReportService : IStudentReportService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IWebHostEnvironment _env;

        public readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public StudentReportService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IAuthorizationService authorizationService, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _env = env;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public void PostStudentReport(StudentReportPostDto postStudentReportDto)
        {
            var loginUserId = _userContextService.GetUserId;
            var reportTopicIdInteger = int.Parse(postStudentReportDto.ReportTopicId);
            var isIndividualBoolean = bool.Parse(postStudentReportDto.IsIndividual);

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

            /*if (putStudentReportDto.Files is null) return;

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
            }*/
            reportToEdit.LastModified = DateTime.UtcNow;
            _dbContext.SaveChanges();
        }

        public async Task<List<StudentReportFile>> UploadFile(int? studentReportId, List<IFormFile> files)
        {
            var uploadResults = new List<StudentReportFile>();
            foreach (var file in files)
            {
                var uploadResult = new StudentReportFile();
                string trustedFileNameForFileStorage;
                string originalFileName = file.FileName;
                uploadResult.FileName = originalFileName;

                trustedFileNameForFileStorage = Path.GetRandomFileName();
                var pathToCheck = Path.Combine(_env.ContentRootPath, "Uploads");
                if (!Directory.Exists(pathToCheck))
                {
                    var di = Directory.CreateDirectory(pathToCheck);
                }
                var path = Path.Combine(_env.ContentRootPath, "Uploads", trustedFileNameForFileStorage);

                using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                
                
                uploadResult.FileName = file.FileName;
                uploadResult.StoredFileName = trustedFileNameForFileStorage;
                uploadResult.ContentType = file.ContentType;
                if (studentReportId == -1)
                {
                    var reportId = _dbContext.StudentReport.OrderByDescending(report => report.SentAt).FirstOrDefault().Id;
                    uploadResult.StudentReportId = reportId;
                }
                else
                {
                    uploadResult.StudentReportId = studentReportId;
                }
                uploadResults.Add(uploadResult);
                _dbContext.StudentReportFile.Add(uploadResult);
            }
            
            _dbContext.SaveChanges();
            return uploadResults;
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
                        .Where(reportTopic => reportTopic.SubjectGroup.TeacherId == teacherId);

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

            

