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

namespace SystemSprawozdan.Backend.Services
{
    public interface ISubjectGroupService
    {
        List<SubjectGroupGetDto> GetSubjectGroup(int subjectId, bool? isUser);
        List<SubjectGroupGetDto> GetSubjectGroupTeacher(int subjectId);
        SubjectGroupGetDetailsDto GetSubjectGroupDetails(int groupId);
        List<StudentBasicGetDto> GetSubjectGroupStudents(int groupId);
        void DeleteStudentFromGroup(int studentId, int groupId);
        public SubjectGroup AddPlaceholderSubjectGroup(int subjectId);
        void CreateSubjectGroup(SubjectGroupPostDto newGroup);
        int GetSubjectId(int groupId);

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
        //! Pobieranie grup, do których należy, bądź nie należy student
        public List<SubjectGroupGetDto> GetSubjectGroup(int subjectId, bool? isUserBelong)
        {
            var loginUserId = _userContextService.GetUserId;

            var subjectGroupsFromDb = _dbContext.SubjectGroup
                .Include(subjectGroup => subjectGroup.Subject)
                    .ThenInclude(subject => subject.Major)
                .Include(subjectGroup => subjectGroup.Teacher)
                .Where(subjectGroup => subjectGroup.SubjectId == subjectId);

            if (isUserBelong == true)
                subjectGroupsFromDb = subjectGroupsFromDb.Where(subjectGroup =>
                    subjectGroup.SubjectSubgroups.Any(subjectSubgroup =>
                        subjectSubgroup.Students.Any(student => student.Id == loginUserId)));
            else if (isUserBelong == false)
                subjectGroupsFromDb = subjectGroupsFromDb.Where(subjectGroup =>
                    !subjectGroup.SubjectSubgroups.Any(subjectSubgroup =>
                        subjectSubgroup.Students.Any(student => student.Id == loginUserId)));

            var subjectGroups = subjectGroupsFromDb.ToList();
            var subjectGroupsDto = _mapper.Map<List<SubjectGroupGetDto>>(subjectGroups);
            return subjectGroupsDto;
        }
        //! Pobieranie grup, które prowadzi dany nauczyciel 
        public List<SubjectGroupGetDto> GetSubjectGroupTeacher(int subjectId)
        {
            var loginUserId = _userContextService.GetUserId;

            var subjectGroupsFromDb = _dbContext.SubjectGroup
                .Include(subjectGroup => subjectGroup.Subject)
                    .ThenInclude(subject => subject.Major)
                .Include(subjectGroup => subjectGroup.Teacher)
                .Where(subjectGroup => subjectGroup.SubjectId == subjectId);

            subjectGroupsFromDb = subjectGroupsFromDb.Where(subjectGroup =>
                subjectGroup.TeacherId == loginUserId);

            var subjectGroups = subjectGroupsFromDb.ToList();
            var subjectGroupsDto = _mapper.Map<List<SubjectGroupGetDto>>(subjectGroups);
            return subjectGroupsDto;
        }
        //! Pobiera szczegóły do danej grupy
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
        //! Pobiera studentów z grupy 
        public List<StudentBasicGetDto> GetSubjectGroupStudents(int groupId)
        {
            if (!(_dbContext.SubjectGroup.Any(group => group.Id == groupId)))
            {
                throw new NotFoundException("Wrong group Id!");
            }
            var studentsFromGroup = new List<StudentBasicGetDto>();

            var subgroups = _dbContext.SubjectSubgroup
                .Include(subjectSubgroup => subjectSubgroup.Students)
                .Where(subgroup => subgroup.SubjectGroupId == groupId)
                .ToList();
            foreach (var subgroup in subgroups)
            {
                var students = subgroup.Students;
                foreach (var student in students)
                {
                    studentsFromGroup.Add(new StudentBasicGetDto
                    {
                        Id = student.Id,
                        Login = student.Login,
                        Name = student.Name,
                        Surname = student.Surname
                    });
                }
            }
            return studentsFromGroup;
        }
        //! Usuwa studentów z grupy 
        public void DeleteStudentFromGroup(int studentId, int groupId)
        {
            if (!(_dbContext.SubjectGroup.Any(group => group.Id == groupId)))
            {
                throw new NotFoundException("Wrong group Id!");
            }
            if (!(_dbContext.Student.Any(student => student.Id == studentId)))
            {
                throw new NotFoundException("Wrong student Id!");
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
                if (subgroup.Students.Count == 0)
                {
                    var group = _dbContext.SubjectGroup.FirstOrDefault(group => group.Id == groupId);
                    group.SubjectSubgroups.Remove(subgroup);
                    _dbContext.SubjectGroup.Update(group);
                }
            }
            _dbContext.SaveChanges();
        }
        //! Dodaje pustą grupę
        public SubjectGroup AddPlaceholderSubjectGroup(int subjectId)
        {
            var teacherId = _userContextService.GetUserId;

            var SubjectGroupToAdd = new SubjectGroup
            {
                Name = "L00",
                GroupType = "Laboratorium",
                SubjectId = subjectId,
                TeacherId = teacherId.Value
            };

            _dbContext.SubjectGroup.Add(SubjectGroupToAdd);
            _dbContext.SaveChanges();

            return SubjectGroupToAdd;
        }

        //! Tworzy nową grupę 
		public void CreateSubjectGroup(SubjectGroupPostDto newGroup)
		{
			var Id = _userContextService.GetUserId;
			var Teacher = _dbContext.Teacher.FirstOrDefault(t => t.Id == Id);
			var group = new SubjectGroup();
			group.Name = newGroup.Name;
			group.GroupType = newGroup.Type;
			group.SubjectId = newGroup.SubjectId;
			group.TeacherId = Teacher.Id;

			_dbContext.SubjectGroup.Add(group);
			_dbContext.SaveChanges();
		}

        //! Zwraca id przedmiotu danej grupy
        public int GetSubjectId(int groupId)
        {
            var subjectGroup = _dbContext.SubjectGroup.FirstOrDefault(group => group.Id == groupId);

            return subjectGroup.SubjectId;
        }
    }
}
