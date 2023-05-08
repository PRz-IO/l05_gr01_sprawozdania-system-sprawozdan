using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentReportController : ControllerBase
    {
        public readonly IStudentReportService _studentReportService;

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

        //TODO: KUSZO: Trzeba stworzyc GETa, ktory wyswietla wszystkie tematy sprawozdan, ktore sa przypisane do danego prowadzacego, ktory jest zalogowany
        [HttpGet]
        public ActionResult<IEnumerable<ReportTopicDto>> GetAllReports()
        {
            var result = _studentReportService.GetAllReports();
            return Ok(result);
        }
    }
}
