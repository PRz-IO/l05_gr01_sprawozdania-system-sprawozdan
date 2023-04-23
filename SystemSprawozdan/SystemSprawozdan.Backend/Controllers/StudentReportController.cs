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
        public ActionResult SendStudentReport([FromBody] SendStudentReportDto sendStudentReportDto)
        {
            _studentReportService.SendStudentReport(sendStudentReportDto);
            return Ok();
        }
        //TODO: Jakub: Trzeba stworzyć POSTa, który umożliwi dodanie sprawozdania do odpowiedniego tematu sprawozdania, przez zalogowanego użytkownika (umieszczenie pliku oraz komentarza)


        [HttpPut("{id}")]
        public ActionResult EditStudentReport([FromRoute] int studentReportId)
        {
            _studentReportService.EditStudentReport(studentReportId);
            return Ok();
        }

        //TODO: Jakub: Trzeba stworzyć PUTa, który umożliwi edycję oddanego sprawozdania, czyli możliwość dodania kolejnego pliku
    }
}
