using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using SystemSprawozdan.Backend.Controllers;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using MimeKit.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace SystemSprawozdan.Backend.Services
{
    public interface ISubjectGroupService
    {
        List<SubjectGroupGetDto> GetUserGroupBelong(int subjectId);
        List<SubjectGroupGetDto> GetUserGroupDoesntBelong(int subjectId);
    }

    public class SubjectGroupService : ISubjectGroupService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public SubjectGroupService(ApiDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }
        public List<SubjectGroupGetDto> GetUserGroupBelong(int subjectId)
        {
            var loginUserId = _userContextService.GetUserId;

            var subjectGroups = _dbContext.SubjectGroup
                .Where(subjectGroup => subjectGroup.SubjectId == subjectId)
                .Where(subjectGroup => subjectGroup.subjectSubgroups
                    .Any(subjectSubgroup => subjectSubgroup.Students
                        .Any(student => student.Id == loginUserId)
                    )
                ).ToList();

            List<SubjectGroupGetDto> subjectGroupsDto = new(); 

            foreach(var subjectGroup in subjectGroups) 
            {
                var teachers = _dbContext.Teacher
                    .Where(teacher => teacher.SubjectGroups
                        .Any(teacherSubjectGroup => teacherSubjectGroup.Id == subjectGroup.Id)
                    ).ToList();

                List<GetTeacherDto> getTeachersDto = new();
                foreach (var teacher in teachers)
                {
                    getTeachersDto.Add(new GetTeacherDto()
                    {
                        Name = teacher.Name,
                        Surname = teacher.Surname,
                        Degree = teacher.Degree,
                    });
                }

                subjectGroupsDto.Add(new SubjectGroupGetDto()
                {
                    Id = subjectGroup.Id,
                    SubjectId = subjectGroup.SubjectId,
                    Name = subjectGroup.Name,
                    Teachers = getTeachersDto,
                    GroupType = subjectGroup.GroupType,
                });
            }

            return subjectGroupsDto;
        }

        public List<SubjectGroupGetDto> GetUserGroupDoesntBelong(int subjectId)
        {
            var loginUserId = _userContextService.GetUserId;

            var userSubgroups = _dbContext.SubjectSubgroup
                .Where(userSubgroup => userSubgroup.Students
                   .Any(user => user.Id == loginUserId)
                );


            var subjectGroups = _dbContext.SubjectGroup.Include(subjectSubgroup => subjectSubgroup.subjectSubgroups)
                .Where(subjectGroup => subjectGroup.SubjectId == subjectId)
                .Where(subjectGroup => userSubgroups
                    .Any(userSubgroup => userSubgroup.SubjectGroupId == subjectGroup.Id)
                   
                ).ToList();

            List<SubjectGroupGetDto> subjectGroupsDto = new();

            foreach (var subjectGroup in subjectGroups)
            {
                var teachers = _dbContext.Teacher
                    .Where(teacher => teacher.SubjectGroups
                        .Any(teacherSubjectGroup => teacherSubjectGroup.Id == subjectGroup.Id)
                    ).ToList();

                List<GetTeacherDto> getTeachersDto = new();
                foreach (var teacher in teachers)
                {
                    getTeachersDto.Add(new GetTeacherDto()
                    {
                        Name = teacher.Name,
                        Surname = teacher.Surname,
                        Degree = teacher.Degree,
                    });
                }
                List<GetSubgroupsDto> getSubgroupsDto =new();

                foreach(var subGroup in subjectGroup.subjectSubgroups.ToList()) 
                {
                    getSubgroupsDto.Add(new GetSubgroupsDto()
                    {
                        Id = subGroup.Id,
                        Name = subGroup.Name,
                    });
                }

                subjectGroupsDto.Add(new SubjectGroupGetDto()
                {
                    Id = subjectGroup.Id,
                    SubjectId = subjectGroup.SubjectId,
                    Name = subjectGroup.Name,
                    Teachers = getTeachersDto,
                    GroupType = subjectGroup.GroupType,
                    Subgroups = getSubgroupsDto,
                });
            }

            return subjectGroupsDto;

        }
    }
}
