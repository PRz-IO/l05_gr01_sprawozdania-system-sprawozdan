using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectGroupController : ControllerBase
    {
        public readonly ISubjectGroupService _subjectGorupServices;

        public SubjectGroupController(ISubjectGroupService subjectGroupService) 
        { 
            _subjectGorupServices =  subjectGroupService;
        }

        //TODO: Mateusz: Trzeba zrobić GETa, który wyświetla wszystkie grupy, do których nalezy i nie należy dany użytkownik
        [HttpGet("{subjectId}")]
        public ActionResult GetSubjectGroup([FromRoute] int subjectId, [FromQuery] bool isUser)
        {
            var subjectGroup = _subjectGorupServices.GetSubjectGroup(subjectId, isUser);
            return Ok(subjectGroup);
        }
    }
}
