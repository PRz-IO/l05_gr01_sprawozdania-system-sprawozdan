﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Controllers;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;
using MimeKit.Cryptography;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface ISubjectGroupService
    {
        List<SubjectGroupGetDto> GetSubjectGroup(int subjectId, bool isUser);
    }

    public class SubjectGroupService : ISubjectGroupService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public SubjectGroupService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public List<SubjectGroupGetDto> GetSubjectGroup(int subjectId, bool isUserBelong)
        {
            var loginUserId = _userContextService.GetUserId;

            var subjectGroupsFromDb = _dbContext.SubjectGroup
                .Include(subjectGroup => subjectGroup.Subject)
                    .ThenInclude(subject => subject.Major)
                .Include(subjectGroup => subjectGroup.Teacher)
                .Where(subjectGroup => subjectGroup.SubjectId == subjectId);

            if (isUserBelong)
                subjectGroupsFromDb = subjectGroupsFromDb.Where(subjectGroup =>
                    subjectGroup.subjectSubgroups.Any(subjectSubgroup =>
                        subjectSubgroup.Students.Any(student => student.Id == loginUserId)));
            else
                subjectGroupsFromDb = subjectGroupsFromDb.Where(subjectGroup =>
                    !subjectGroup.subjectSubgroups.Any(subjectSubgroup =>
                        subjectSubgroup.Students.Any(student => student.Id == loginUserId)));

            var subjectGroups = subjectGroupsFromDb.ToList();
            var subjectGroupsDto = _mapper.Map<List<SubjectGroupGetDto>>(subjectGroups);
            return subjectGroupsDto;
        }
    }
}
