using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentReportController : ControllerBase
    {
        public readonly IStudentReportService _studentReportService;
        //TODO: Jakub: Trzeba stworzyć POSTa, który umożliwi dodanie sprawozdania do odpowiedniego tematu sprawozdania, przez zalogowanego użytkownika (umieszczenie pliku oraz komentarza)

        //TODO: Jakub: Trzeba stworzyć PUTa, który umożliwi edycję oddanego sprawozdania, czyli możliwość dodania kolejnego pliku
    }
}
