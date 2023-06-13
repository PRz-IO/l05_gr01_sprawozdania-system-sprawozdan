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
        public ActionResult GetSubjectGroup([FromRoute] int subjectId, [FromQuery] bool? isUser)
        {
            var subjectGroup = _subjectGorupServices.GetSubjectGroup(subjectId, isUser);
            return Ok(subjectGroup);
        }
        [HttpGet("{subjectId}/Teacher")]
        public ActionResult GetSubjectGroupTeacher([FromRoute] int subjectId)
        {
            var subjectGroup = _subjectGorupServices.GetSubjectGroupTeacher(subjectId);
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

        [HttpPost("placeholder/{subjectId:int}")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult AddPlaceholderSubjectGroup([FromRoute] int subjectId)
        {
            var createdObject = _subjectGorupServices.AddPlaceholderSubjectGroup(subjectId);

            var result = new
            {
                Id = createdObject.Id,
                Name = createdObject.Name,
                GroupType = createdObject.GroupType,
                SubjectId = createdObject.SubjectId,
                TeacherId = createdObject.TeacherId
            };

            return Created($"{subjectId}", result);
        }

        [HttpPost("CreateSubjectGroup")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult CreateSubjectGroup([FromBody] SubjectGroupPostDto newGroup)
        {
            _subjectGorupServices.CreateSubjectGroup(newGroup);
            return Ok();
        }

        [HttpGet("{groupId}/GetSubjectId")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult GetSubjectId([FromRoute] int groupId)
        {
            var subjectId = _subjectGorupServices.GetSubjectId(groupId);
            return Ok(subjectId);
        }
    }

}
