using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Enums;

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
        [Authorize(Roles = nameof(UserRoleEnum.Student))]
        public ActionResult GetSubjectGroup([FromRoute] int subjectId, [FromQuery] bool isUser)
        {
            var subjectGroup = _subjectGorupServices.GetSubjectGroup(subjectId, isUser);
            return Ok(subjectGroup);
        }
        
        [HttpGet("{groupId}/GetSubjectGroupDetails")]
        public ActionResult GetSubjectGroupDetails([FromRoute] int groupId)
        {
            var subjectGroupDetails = _subjectGorupServices.GetSubjectGroupDetails(groupId);
            return Ok(subjectGroupDetails);
        }

        [HttpGet("{groupId}/GetStudentsFromGroup")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult GetStudentsFromGroup([FromRoute] int groupId)
        {
            var studentsFromGroup = _subjectGorupServices.GetSubjectGroupStudents(groupId);
            return Ok(studentsFromGroup);
        }
        [HttpDelete("{groupId}/RemoveStudentFromGroup/{studentId}")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult RemoveStudentFromGroup([FromRoute] int studentId, [FromRoute] int groupId)
        {
            _subjectGorupServices.DeleteStudentFromGroup(studentId, groupId);
            return Ok();
        }

        [HttpPost("CreateSubjectGroup")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult CreateSubjectGroup([FromBody] SubjectGroupPostDto newGroup)
        {
            _subjectGorupServices.CreateSubjectGroup(newGroup);
            return Ok();
        }

    }
}
