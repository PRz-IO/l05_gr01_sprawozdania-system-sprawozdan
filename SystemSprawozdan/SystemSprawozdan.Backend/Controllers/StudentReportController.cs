using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public ActionResult<IEnumerable<ReportTopicDto>> GetReports([FromQuery] bool? toCheck)
        {
            var result = _studentReportService.GetReports(toCheck);
            return Ok(result);
        }

        //TODO: Paweł: Trzeba stworzyc GETa, ktory wyswietla wszystkie sprawozdania, ktore sa przypisane do danej grupy
        [HttpGet("{reportTopicId}/Individual")]
        public ActionResult<IEnumerable<ReportTopicDto>> GetIndividualReportsByTopicId([FromRoute] int reportTopicId, [FromQuery] bool? isMarked)
        {
            var result = _studentReportService.GetIndividualReportsByTopicId(reportTopicId, isMarked);
            return Ok(result);
        }

        //TODO: Paweł: Trzeba stworzyc GETa, ktory wyswietla wszystkie sprawozdania, ktore sa przypisane do danej grupy
        [HttpGet("{reportTopicId}/Collaborate")]
        public ActionResult<IEnumerable<ReportTopicDto>> GetCollaborateReportsByTopicId([FromRoute] int reportTopicId, [FromQuery] bool? isMarked)
        {
            var result = _studentReportService.GetCollaborateReportsByTopicId(reportTopicId, isMarked);
            return Ok(result);
        }
    }
}
