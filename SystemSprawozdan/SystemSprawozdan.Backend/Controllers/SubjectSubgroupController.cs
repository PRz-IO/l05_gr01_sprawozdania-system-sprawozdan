using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectSubgroupController : ControllerBase
    {
        private readonly ISubjectSubgroupService _subjectSubgroupService;

        public SubjectSubgroupController(ISubjectSubgroupService subjectSubgroupService)
        {
            _subjectSubgroupService = subjectSubgroupService;
        }
        //TODO: Grzesiek: Trzeba zrobić POSTa, który będzie dodawał nową podgrupę, przy zapisie do zespołu o danym ID (jezeli to bedzie bedzie grupa indywidualna to trzeba to gdzies oznaczyc a jezeli grupowa to miec mozliwosc wppisania jakies nazwy)
        [HttpPost]
        public ActionResult CreateSubgroup([FromBody] CreateSubgroupDto createSubgroupDto)
        {
            _subjectSubgroupService.CreateSubgroup(createSubgroupDto);
            return Ok();
        }
        //TODO: Grzesiek: Trzeba zrobić GETa, który będzie wyświetlał wszystkie podgrupy typu "grupowego" dla danej grupy zajęciowej
        [HttpGet("{GroupId}/GetSubgroups")]
        public ActionResult GetSubgroups(int GroupId)
        {
            var result = _subjectSubgroupService.GetSubgroups(GroupId);
            return Ok(result);
        }
        //TODO: Grzesiek: Trzeba zrobić PUTa, który będzie do danej podgrupy, która jest typu "grupowego", dodawał kolejnych zalogowanych uzytkownikow
        [HttpPut("{SubgroupId}/AddUserToSubgroup")]
        public ActionResult AddUserToSubgroup(int SubgroupId)
        {
            _subjectSubgroupService.AddUserToSubgroup(SubgroupId);
            return Ok();
        }
    }
}
