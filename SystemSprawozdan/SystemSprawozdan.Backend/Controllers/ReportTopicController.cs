using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Dto.ReportTopic;
using SystemSprawozdan.Shared.Enums;


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
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
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
        [HttpGet("ForStudent")]
        public IActionResult GetReportTopicsByUserId([FromQuery] bool isSubmitted)
        {
            var reportTopics = _reportTopicService.GetReportTopicForStudent(isSubmitted);
            return Ok(reportTopics);
        }


        [HttpGet("selective")]
        public ActionResult GetReportTopic([FromQuery] int? reportTopicId, int? studentReportId)
        {
            var reportTopic = _reportTopicService.GetReportTopic(reportTopicId, studentReportId);

            return Ok(reportTopic);


        }

        [HttpGet("{groupId}/GetTopicsForGroup")]
        public ActionResult GetReportTopicForGroup([FromRoute] int groupId)
        {
            var reportTopics = _reportTopicService.GetReportTopicForGroup(groupId);

            return Ok(reportTopics);
        }
        [HttpPost("AddTopic")]
        public ActionResult PostReportTopic([FromBody] ReportTopicPostDto reportTopic)
        {
            _reportTopicService.PostReportTopic(reportTopic);
            return Ok();
        }
        
        [HttpGet("ForTeacher")]
        public IActionResult GetSubmittedReportsByStudentAndSubject(int studentId, int subjectId)
        {
            var submittedReports = _reportTopicService.GetSubmittedReportsByStudentAndSubject(studentId, subjectId);
            return Ok(submittedReports);
        }
    }
}
