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

        [HttpGet]
        public ActionResult GetReportTopic([FromQuery] int? reportTopicId, int? studentReportId)
        {
            var reportTopic = _reportTopicService.GetReportTopic(reportTopicId, studentReportId);
            if (reportTopic is null) return BadRequest($"Brak tematu sprawozdania o ID: {reportTopicId}!");

            return Ok(reportTopic);

            
        }
    }
}
