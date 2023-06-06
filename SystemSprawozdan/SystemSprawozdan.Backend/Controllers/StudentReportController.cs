using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Enums;

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
        [Authorize(Roles = nameof(UserRoleEnum.Student))]
        public ActionResult PostStudentReport([FromBody] StudentReportPostDto postStudentReportDto)
        {
            var result = _studentReportService.PostStudentReport(postStudentReportDto);
            return Ok(result.Id);
        }

        [HttpPut("{studentReportId:int}/AsStudent")]
        [Authorize(Roles = nameof(UserRoleEnum.Student))]
        public ActionResult PutStudentReportAsStudent([FromRoute] int studentReportId, [FromBody] StudentReportAsStudentPutDto studentReportAsStudentPutDto)
        {
             _studentReportService.PutStudentReportAsStudent(studentReportId, studentReportAsStudentPutDto);
            return Ok();
        }

        [HttpPut("{studentReportId:int}/AsTeacher")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult PutStudentReportAsTeacher([FromRoute] int studentReportId, [FromBody] StudentReportAsTeacherPutDto studentReportAsTeacherPutDto)
        {
             _studentReportService.PutStudentReportAsTeacher(studentReportId, studentReportAsTeacherPutDto);
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
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult<IEnumerable<StudentReportGetDto>> GetCollaborateReportsByTopicId([FromRoute] int reportTopicId, [FromQuery] bool? isIndividual, [FromQuery] bool? toCheck)
        {
            var result = _studentReportService.GetStudentReportsByTopicId(reportTopicId, isIndividual, toCheck);
            return Ok(result);
        }

        //TODO: Paweł: Trzeba stworzyc GETa, ktory wyswietla wszystkie sprawozdania, ktore sa przypisane do danej grupy
        [HttpGet("/api/ReportTopic/{reportTopicId}/StudentReports/StudentsWithoutReport")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult<IEnumerable<StudentBasicGetDto>> GetStudentWithoutReportByTopicId([FromRoute] int reportTopicId)
        {
            var result = _studentReportService.GetStudentWithoutReportByTopicId(reportTopicId);
            return Ok(result);
        }
    }
}
