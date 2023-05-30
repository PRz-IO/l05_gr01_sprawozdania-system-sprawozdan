using Microsoft.EntityFrameworkCore;
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
using SystemSprawozdan.Shared.Dto.SubjectGroup;

namespace SystemSprawozdan.Backend.Services
{
    public interface ISubjectGroupService
    {
        List<SubjectGroupGetDto> GetSubjectGroup(int subjectId, bool isUser);
        SubjectGroupGetDetailsDto GetSubjectGroupDetails(int groupId);
        List<SubjectGroupGetStudentsDto> GetSubjectGroupStudents(int groupId);
        void DeleteStudentFromGroup(int studentId, int groupId);
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
        public SubjectGroupGetDetailsDto GetSubjectGroupDetails(int groupId)
        {
            var details = new SubjectGroupGetDetailsDto();

            var group = _dbContext.SubjectGroup.FirstOrDefault(group => group.Id == groupId);
            details.SubjectName = _dbContext.Subject.FirstOrDefault(subject => subject.Id == group.SubjectId).Name;
            details.GroupType = group.GroupType;
            details.Name = group.Name;
            var Teacher = _dbContext.Teacher.FirstOrDefault(teacher => teacher.Id == group.TeacherId);
            var TeacherName = Teacher.Degree + " " + Teacher.Name + " " + Teacher.Surname;
            details.TeacherName = TeacherName;
            return details;
        }
        public List<SubjectGroupGetStudentsDto> GetSubjectGroupStudents(int groupId)
        {
            if (!(_dbContext.SubjectGroup.Any(group => group.Id == groupId)))
            {
                throw new NotFoundException("Wrong group id!");
            }
            var studentsFromGroup = new List<SubjectGroupGetStudentsDto>();

            var subgroups = _dbContext.SubjectSubgroup
                .Include(subjectSubgroup => subjectSubgroup.Students)
                .Where(subgroup => subgroup.SubjectGroupId == groupId)
                .ToList();
            foreach (var subgroup in subgroups)
            {
                var students = subgroup.Students;
                foreach(var student in students)
                {
                    studentsFromGroup.Add(new SubjectGroupGetStudentsDto
                    {
                        Index = student.Id,
                        Name = student.Name,
                        Surname = student.Surname
                    });
                }
            }
                return studentsFromGroup;
        }
        public void DeleteStudentFromGroup(int studentId, int groupId)
        {
            if (!(_dbContext.SubjectGroup.Any(group => group.Id == groupId)))
            {
                throw new NotFoundException("Wrong group id!");
            }
            if (!(_dbContext.Student.Any(student => student.Id == studentId)))
            {
                throw new NotFoundException("Wrong student id!");
            }
            var subgroups = _dbContext.SubjectSubgroup
                .Include(subjectSubgroup => subjectSubgroup.Students)
                .Where(subgroup => subgroup.SubjectGroupId == groupId)
                .ToList();
            var student = _dbContext.Student.FirstOrDefault(student => student.Id == studentId);
            foreach (var subgroup in subgroups)
            {
                subgroup.Students.Remove(student);
                _dbContext.SubjectSubgroup.Update(subgroup);
                if(subgroup.Students.Count == 0)
                {
                    var groups = _dbContext.SubjectGroup.Where(c => c.Id == groupId).ToList();
                    foreach (var group in groups)
                    {
                        group.subjectSubgroups.Remove(subgroup);
                        _dbContext.SubjectGroup.Update(group);
                    }
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
