using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportTopicController : ControllerBase
    {
        private readonly IReportTopicService _reportTopicService;

        public ReportTopicController(IReportTopicService reportTopicService)
        {
            _reportTopicService = reportTopicService;
        }
        //TODO: Olek: Trzeba stworzyć GETa, ktory wyswietla wszystkie tematy sprawozdan, które są przypisane do SubjectGroup, do której należy zalogowany użytkownik

        //TODO: Olek: Trzeba stworzyć GETa, który wyświetla wszystkie tematy sprawozdan, które są oddane przez zalogowanego użytkownika

        [HttpGet("{reportTopicId}")]
        public ActionResult GetReportTopic([FromRoute] int reportTopicId)
        {
            var reportTopic = _reportTopicService.GetReportTopic(reportTopicId);
            if (reportTopic is null) return BadRequest("Brak tematu sprawozdania o danym ID!");

            return Ok(reportTopic);

            
        }
    }
}
