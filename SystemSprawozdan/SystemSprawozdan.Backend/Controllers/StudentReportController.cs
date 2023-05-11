using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentReportController : ControllerBase
    {
        private readonly IStudentReportService _studentReportService;

        public StudentReportController(IStudentReportService studentReportService)
        {
            _studentReportService = studentReportService;
        }

        [HttpPost]
        public ActionResult PostStudentReport([FromForm] StudentReportPostDto postStudentReportDto)
        {
            _studentReportService.PostStudentReport(postStudentReportDto);
            return Ok();
        }

        [HttpPut("{studentReportId}")]
        public ActionResult PutStudentReport([FromRoute] int studentReportId, [FromForm] StudentReportPutDto putStudentReportDto)
        {
             _studentReportService.PutStudentReport(studentReportId, putStudentReportDto);
            return Ok();
        }

        [HttpPost("files/{studentReportId:int?}")]
        [RequestSizeLimit(524288000)] // 500Mb
        public async Task<ActionResult<List<StudentReportFile>>> UploadFile([FromForm] List<IFormFile> files, int studentReportId = -1)
        {
            var result = await _studentReportService.UploadFile(studentReportId, files);
            return Ok();
        }

        //TODO: KUSZO: Trzeba stworzyc GETa, ktory wyswietla wszystkie tematy sprawozdan, ktore sa przypisane do danego prowadzacego, ktory jest zalogowany
        [HttpGet]
        public ActionResult<IEnumerable<ReportTopicDto>> GetReports([FromQuery] bool? toCheck)
        {
            var result = _studentReportService.GetReports(toCheck);
            return Ok(result);
        }
    }
}
