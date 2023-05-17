using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentReportFileController : ControllerBase
    {
        private readonly IStudentReportFileService _studentReportFileService;
        private readonly ApiDbContext _dbContext;
        private readonly IWebHostEnvironment _env;

        public StudentReportFileController(IStudentReportFileService studentReportFileService, ApiDbContext dbContext,
            IWebHostEnvironment env)
        {
            _studentReportFileService = studentReportFileService;
            _dbContext = dbContext;
            _env = env;
        }
        
        [HttpGet("download/{randomFileName}")]
        public async Task<ActionResult> DownloadFile(string randomFileName)
        {
            var uploadResult = await _dbContext.StudentReportFile.FirstOrDefaultAsync(u => u.StoredFileName == randomFileName);
            
            var path = Path.Combine(_env.ContentRootPath, "Uploads", randomFileName);
            
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, uploadResult.ContentType, Path.GetFileName(path));
            // Wykonanie lini kodu powyżej możliwe tylko bezpośrednio w kontrolerze (File), dlatego kontroler jest taki gruby.
            // Patrz link: https://stackoverflow.com/questions/35936628/non-invocable-member-file-cannot-be-used-like-a-method-while-generating-report
        }

        [HttpGet("{studentReportId}")]
        public ActionResult GetStudentReportFile([FromRoute] int studentReportId)
        {
            var result = _studentReportFileService.GetStudentReportFile(studentReportId);
            return Ok(result);
        }

        [HttpPost("{studentReportId:int?}")]
        [RequestSizeLimit(524288000)] // 500Mb
        public async Task<ActionResult<List<StudentReportFileController>>> UploadFile([FromForm] List<IFormFile> files, int studentReportId = -1)
        {
            var result = await _studentReportFileService.UploadFile(studentReportId, files);
            return Ok();
        }
    }
}
