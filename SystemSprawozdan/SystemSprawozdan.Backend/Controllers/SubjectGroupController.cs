using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectGroupController : ControllerBase
    {
        public readonly ISubjectGroupService _subjectGorupServices;

        public SubjectGroupController(ISubjectGroupService subjectGroupService) 
        { 
            _subjectGorupServices =  subjectGroupService;
        }

        //TODO: Mateusz: Trzeba zrobić GETa, który wyświetla wszystkie grupy, do których nie należy dany użytkownik
        [HttpGet("{subjectId}/isNotUser")]
        public ActionResult GetUserGroupDoesntBelong([FromRoute] int subjectId)
        {

            var  subjectGroup = _subjectGorupServices.GetUserGroupDoesntBelong(subjectId);
            return Ok(subjectGroup);
        }

        //TODO: Mateusz: Trzeba zrobić GETa, który wyświetla wszystkie grupy, do których należy dany użytkownik
        [HttpGet("{subjectId}/isUser")]
        public ActionResult GetUserGroupBelong([FromRoute] int subjectId)
        {
            var subjectGroup = _subjectGorupServices.GetUserGroupBelong(subjectId);
            return Ok(subjectGroup);
        }
    }
}
