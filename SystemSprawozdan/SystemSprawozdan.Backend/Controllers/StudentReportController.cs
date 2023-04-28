using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Services;

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
        public ActionResult SendStudentReport([FromForm] SendStudentReportDto sendStudentReportDto)
        {
            _studentReportService.SendStudentReport(sendStudentReportDto);
            return Ok();
        }

        [HttpPut("{studentReportId}")]
        public ActionResult EditStudentReport([FromRoute] int studentReportId, [FromForm] EditStudentReportDto editStudentReportDto)
        {
            _studentReportService.EditStudentReport(studentReportId, editStudentReportDto);
            return Ok();
        }

    }
}
