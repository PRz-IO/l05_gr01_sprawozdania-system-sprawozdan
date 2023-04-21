using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectSubgroupController : ControllerBase
    {
        //TODO: Grzesiek: Trzeba zrobić POSTa, który będzie dodawał nową podgrupę, przy zapisie do zespołu o danym ID (jezeli to bedzie bedzie grupa indywidualna to trzeba to gdzies oznaczyc a jezeli grupowa to miec mozliwosc wppisania jakies nazwy)

        //TODO: Grzesiek: Trzeba zrobić GETa, który będzie wyświetlał wszystkie podgrupy typu "grupowego" dla danej grupy zajęciowej

        //TODO: Grzesiek: Trzeba zrobić PUTa, który będzie do danej podgrupy, która jest typu "grupowego", dodawał kolejnych zalogowanych uzytkownikow

    }
}
