using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentReportController : ControllerBase
    {
        //TODO: Jakub: Trzeba stworzyć POSTa, który umożliwi dodanie sprawozdania do odpowiedniego tematu sprawozdania, przez zalogowanego użytkownika (umieszczenie pliku oraz komentarza)

        //TODO: Jakub: Trzeba stworzyć PUTa, który umożliwi edycję oddanego sprawozdania, czyli możliwość dodania kolejnego pliku
    }
}
