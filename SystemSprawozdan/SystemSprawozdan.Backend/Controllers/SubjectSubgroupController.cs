using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectSubgroupController : ControllerBase
    {
        private readonly ISubjectSubgroupService _subjectSubgroupService;

        public SubjectSubgroupController(ISubjectSubgroupService subjectSubgroupService)
        {
            _subjectSubgroupService = subjectSubgroupService;
        }
        //TODO: Grzesiek: Trzeba zrobić POSTa, który będzie dodawał nową podgrupę, przy zapisie do zespołu o danym ID (jezeli to bedzie bedzie grupa indywidualna to trzeba to gdzies oznaczyc a jezeli grupowa to miec mozliwosc wppisania jakies nazwy)
        [HttpPost]
        public ActionResult CreateSubgroup([FromBody] SubjectSubgroupPostDto createSubgroupDto)
        {
            _subjectSubgroupService.CreateSubgroup(createSubgroupDto);
            return Ok();
        }
        //TODO: Grzesiek: Trzeba zrobić GETa, który będzie wyświetlał wszystkie podgrupy typu "grupowego" dla danej grupy zajęciowej
        [HttpGet("{groupId}/GetSubgroups")]
        public ActionResult GetSubgroups(int groupId)
        {
            var result = _subjectSubgroupService.GetSubgroups(groupId);
            return Ok(result);
        }
        //TODO: Grzesiek: Trzeba zrobić PUTa, który będzie do danej podgrupy, która jest typu "grupowego", dodawał kolejnych zalogowanych uzytkownikow
        [HttpPost("AddUserToSubgroup")]
        public ActionResult AddUserToSubgroup([FromBody] int subgroupId)
        {
            _subjectSubgroupService.AddUserToSubgroup(subgroupId);
            return Ok();
        }
    }
}
