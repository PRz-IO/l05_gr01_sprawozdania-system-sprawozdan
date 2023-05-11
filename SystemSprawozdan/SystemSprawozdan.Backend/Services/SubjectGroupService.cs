using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Controllers;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using MimeKit.Cryptography;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Exceptions;

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
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public SubjectGroupService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
            _authorizationService = authorizationService;
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

            // List<SubjectGroupGetDto> subjectGroupsDto = new(); 
            //
            // foreach(var subjectGroup in subjectGroups) 
            // {
            //     var teachers = _dbContext.Teacher
            //         .Where(teacher => teacher.SubjectGroups
            //             .Any(teacherSubjectGroup => teacherSubjectGroup.Id == subjectGroup.Id)
            //         ).ToList();
            //
            //     List<GetTeacherDto> getTeachersDto = new();
            //     foreach (var teacher in teachers)
            //     {
            //         getTeachersDto.Add(new GetTeacherDto()
            //         {
            //             Name = teacher.Name,
            //             Surname = teacher.Surname,
            //             Degree = teacher.Degree,
            //         });
            //     }
            //
            //     subjectGroupsDto.Add(new SubjectGroupGetDto()
            //     {
            //         Id = subjectGroup.Id,
            //         SubjectId = subjectGroup.SubjectId,
            //         Name = subjectGroup.Name,
            //         Teachers = getTeachersDto,
            //         GroupType = subjectGroup.GroupType,
            //     });
            // }

            var subjectGroupsDto = _mapper.Map<List<SubjectGroupGetDto>>(subjectGroups);

            return subjectGroupsDto;
        }

        public List<SubjectGroupGetDto> GetUserGroupDoesntBelong(int subjectId)
        {
            // var userSubgroups = _dbContext.SubjectSubgroup
            //     .Where(userSubgroup => userSubgroup.Students
            //        .Any(user => user.Id == loginUserId)
            //     );
            //
            //
            // var subjectGroups = _dbContext.SubjectGroup.Include(subjectSubgroup => subjectSubgroup.subjectSubgroups)
            //     .Where(subjectGroup => subjectGroup.SubjectId == subjectId)
            //     .Where(subjectGroup => userSubgroups
            //         .Any(userSubgroup => userSubgroup.SubjectGroupId == subjectGroup.Id)
            //        
            //     ).ToList();
            //
            // List<SubjectGroupGetDto> subjectGroupsDto = new();
            //
            // foreach (var subjectGroup in subjectGroups)
            // {
            //     var teachers = _dbContext.Teacher
            //         .Where(teacher => teacher.SubjectGroups
            //             .Any(teacherSubjectGroup => teacherSubjectGroup.Id == subjectGroup.Id)
            //         ).ToList();
            //
            //     List<GetTeacherDto> getTeachersDto = new();
            //     foreach (var teacher in teachers)
            //     {
            //         getTeachersDto.Add(new GetTeacherDto()
            //         {
            //             Name = teacher.Name,
            //             Surname = teacher.Surname,
            //             Degree = teacher.Degree,
            //         });
            //     }
            //     List<GetSubgroupsDto> getSubgroupsDto =new();
            //
            //     foreach(var subGroup in subjectGroup.subjectSubgroups.ToList()) 
            //     {
            //         getSubgroupsDto.Add(new GetSubgroupsDto()
            //         {
            //             Id = subGroup.Id,
            //             Name = subGroup.Name,
            //         });
            //     }
            //
            //     subjectGroupsDto.Add(new SubjectGroupGetDto()
            //     {
            //         Id = subjectGroup.Id,
            //         SubjectId = subjectGroup.SubjectId,
            //         Name = subjectGroup.Name,
            //         Teachers = getTeachersDto,
            //         GroupType = subjectGroup.GroupType,
            //         Subgroups = getSubgroupsDto,
            //     });
            // }
            
            var authorizationResult = _authorizationService.AuthorizeAsync(
                _userContextService.User,
                null,
                new UserResourceOperationRequirement(UserResourceOperation.Read)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            
            var loginUserId = _userContextService.GetUserId;

            var subjectGroupsFromDb = _dbContext.SubjectGroup
                .Include(subjectGroup => subjectGroup.Subject)
                    .ThenInclude(subject => subject.Major)
                .Include(subjectGroup => subjectGroup.Teacher)
                .Where(subjectGroup => subjectGroup.SubjectId == subjectId)
                .Where(subjectGroup => !subjectGroup.subjectSubgroups.Any(subjectSubgroups => subjectSubgroups.Students.Any(student => student.Id == loginUserId)))
                .ToList();
            
            var subjectGroupsDto = _mapper.Map<List<SubjectGroupGetDto>>(subjectGroupsFromDb);
            
            return subjectGroupsDto;

        }
    }
}
