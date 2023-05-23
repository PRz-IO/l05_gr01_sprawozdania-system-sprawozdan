using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;


namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentReportFileService
    {
        Task<List<StudentReportFile>> UploadFile(int? studentReportId, List<IFormFile> files);
        List<StudentReportFileGetDto> GetStudentReportFile([FromRoute] int studentReportId);
    }
    
    
    public class StudentReportFileService : IStudentReportFileService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IWebHostEnvironment _env;
        
        public readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public StudentReportFileService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IAuthorizationService authorizationService, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _env = env;
            _mapper = mapper;
            _authorizationService = authorizationService;
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

        public List<StudentReportFileGetDto> GetStudentReportFile(int studentReportId)
        {
            var reportsDto = new List<StudentReportFileGetDto>();
            var studentReportFileList = _dbContext.StudentReportFile.Where
                    (report => report.StudentReportId == studentReportId).ToList();
            foreach (var file in studentReportFileList)
            {
                var reportDto = new StudentReportFileGetDto();
                reportDto = _mapper.Map<StudentReportFileGetDto>(file);
                reportsDto.Add(reportDto);
            }
            return reportsDto;
        }
    }
}
