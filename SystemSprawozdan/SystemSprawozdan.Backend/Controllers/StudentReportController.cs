using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
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

        //TODO: Paweł: Trzeba stworzyc GETa, ktory wyswietla wszystkie sprawozdania, ktore sa przypisane do danej grupy
        [HttpGet("/api/ReportTopic/{reportTopicId}/StudentReports")]
        public ActionResult<IEnumerable<StudentReportGetDto>> GetCollaborateReportsByTopicId([FromRoute] int reportTopicId, [FromQuery] bool? isIndividual, [FromQuery] bool? isMarked)
        {
            var result = _studentReportService.GetStudentReportsByTopicId(reportTopicId, isIndividual, isMarked);
            return Ok(result);
        }
    }
}
