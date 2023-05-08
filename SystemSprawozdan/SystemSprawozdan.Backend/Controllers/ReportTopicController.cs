using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportTopicController : ControllerBase
    {
        //TODO: KUSZO: Trzeba stworzyc GETa, ktory wyswietla wszystkie tematy sprawozdan, ktore sa przypisane do danego prowadzacego, ktory jest zalogowany

        //TODO: Olek: Trzeba stworzyć GETa, ktory wyswietla wszystkie tematy sprawozdan, które są przypisane do SubjectGroup, do której należy zalogowany użytkownik

        //TODO: Olek: Trzeba stworzyć GETa, który wyświetla wszystkie tematy sprawozdan, które są oddane przez zalogowanego użytkownika
    }
}
