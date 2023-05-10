using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public readonly ISubjectService _subjectServices;

        public SubjectController(ISubjectService subjectService) 
        { 
            _subjectServices =  subjectService;
        }

        //TODO: Mateusz: Trzeba zrobić GETa, który wyświetla wszystkie przedmioty 
        [HttpGet]
        public ActionResult GetSubjects() 
        {
            var subjects = _subjectServices.GetSubjects();
            return Ok(subjects);
        }
    }
}
