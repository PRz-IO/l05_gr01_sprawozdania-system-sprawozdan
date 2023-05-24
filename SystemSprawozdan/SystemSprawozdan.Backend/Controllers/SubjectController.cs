using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        public readonly ISubjectService _subjectServices;

        public SubjectController(ISubjectService subjectService) 
        { 
            _subjectServices =  subjectService;
        }

        //TODO: Mateusz: Trzeba zrobić GETa, który wyświetla wszystkie przedmioty 
        [HttpGet]
        [Authorize(Roles = nameof(UserRoleEnum.Student))]
        public ActionResult GetSubjects() 
        {
            var subjects = _subjectServices.GetSubjects();
            return Ok(subjects);
        }
    }
}
