using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportTopicController : ControllerBase
    {
        private readonly IReportTopicService _reportTopicService;

        public ReportTopicController(IReportTopicService reportTopicService)
        {
            _reportTopicService = reportTopicService;
        }
        //DONE: KUSZO: Trzeba stworzyc GETa, ktory wyswietla wszystkie tematy sprawozdan, ktore sa przypisane do danego prowadzacego, ktory jest zalogowany
        [HttpGet]
        public ActionResult<IEnumerable<ReportTopicGetDto>> GetReports([FromQuery] bool? toCheck)
        {
            var result = _reportTopicService.GetReports(toCheck);
            return Ok(result);
        }
        
        //DONE: KUSZO: Trzeba stworzyc GETa, ktory wyswietla pojedynczy temat sprawozdania, ktory jest przypisany do danego prowadzacego, ktory jest zalogowany
        [HttpGet("{reportTopicId}")]
        public ActionResult<ReportTopicGetDto> GetReport([FromRoute] int reportTopicId)
        {
            var result = _reportTopicService.GetReportById(reportTopicId);
            return Ok(result);
        }
        
        //TODO: Olek: Trzeba stworzyć GETa, ktory wyswietla wszystkie tematy sprawozdan, które są przypisane do SubjectGroup, do której należy zalogowany użytkownik

        //TODO: Olek: Trzeba stworzyć GETa, który wyświetla wszystkie tematy sprawozdan, które są oddane przez zalogowanego użytkownika

        [HttpGet]
        public ActionResult GetReportTopic([FromQuery] int? reportTopicId, int? studentReportId)
        {
            var reportTopic = _reportTopicService.GetReportTopic(reportTopicId, studentReportId);
            if (reportTopic is null) return BadRequest($"Brak tematu sprawozdania o ID: {reportTopicId}!");

            return Ok(reportTopic);

            
        }
    }
}
