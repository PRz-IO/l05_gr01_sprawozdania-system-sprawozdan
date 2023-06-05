﻿using Microsoft.AspNetCore.Authorization;
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
        public readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService) 
        { 
            _subjectService =  subjectService;
        }

        //TODO: Mateusz: Trzeba zrobić GETa, który wyświetla wszystkie przedmioty 
        [HttpGet]
        [Authorize(Roles = nameof(UserRoleEnum.Student))]
        public ActionResult GetSubjects() 
        {
            var subjects = _subjectService.GetSubjects();
            return Ok(subjects);
        }
        
        
        [HttpGet("forTeacher")]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult<IEnumerable<TeacherSubjectGetDto>> GetTeacherSubjects()
        {
            var result = _subjectService.GetTeacherSubjects();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult AddSubject([FromBody] SubjectPostDto subjectPostDto)
        {
            var createdObject = _subjectService.AddSubject(subjectPostDto);
            var result = new
            {
                Id = createdObject.Id,
                Name = createdObject.Name,
                Description = createdObject.Description,
                MajorId = createdObject.MajorId,
                Term = createdObject.Term
            };
            return Created("https://localhost:7184/api/Subject" , result);

        }
    }
}
