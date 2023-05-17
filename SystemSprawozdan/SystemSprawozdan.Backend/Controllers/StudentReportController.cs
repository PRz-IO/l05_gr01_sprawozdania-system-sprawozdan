using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentReportController : ControllerBase
    {
        private readonly IStudentReportService _studentReportService;
        public StudentReportController(IStudentReportService studentReportService, ApiDbContext dbContext, IWebHostEnvironment env)
        {
            _studentReportService = studentReportService;
        }

        [HttpPost]
        public ActionResult PostStudentReport([FromBody] StudentReportPostDto postStudentReportDto)
        {
            var result = _studentReportService.PostStudentReport(postStudentReportDto);
            return Ok(result.Id);
        }

        [HttpPut("{studentReportId:int}")]
        public ActionResult PutStudentReport([FromRoute] int studentReportId, [FromBody] StudentReportPutDto putStudentReportDto)
        {
             _studentReportService.PutStudentReport(studentReportId, putStudentReportDto);
            return Ok();
        }

        [HttpGet("fullReport/{studentReportId:int}")]
        public ActionResult<StudentReportGetDto> GetStudentReport([FromRoute] int studentReportId)
        {
            var result = _studentReportService.GetStudentReport(studentReportId);
            return Ok(result);
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
