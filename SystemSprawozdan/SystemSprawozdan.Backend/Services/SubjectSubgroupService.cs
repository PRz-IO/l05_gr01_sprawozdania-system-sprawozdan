using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Enums;


namespace SystemSprawozdan.Backend.Services
{
    public interface ISubjectSubgroupService
    {
        void CreateSubgroup(SubjectSubgroupPostDto createSubgroupDto);
        List<SubjectSubgroupGetDto> GetSubgroups(int GroupId);
        void AddUserToSubgroup(int SubgroupId);
    }

    public class SubjectSubgroupService : ISubjectSubgroupService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public SubjectSubgroupService(ApiDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        //metodki
        //! Tworzy podgrupę
        public void CreateSubgroup(SubjectSubgroupPostDto createSubgroupDto)
        {
            var userId = _userContextService.GetUserId;

            var newSubgroup = new SubjectSubgroup()
            {
                SubjectGroupId = createSubgroupDto.SubjectGroupId,
                Students = new List<Student>(),
                Name = createSubgroupDto.SubgroupName,
            };

            if (!createSubgroupDto.isIndividual)
            {
                newSubgroup.IsIndividual = false;
            }
            else
            {
                newSubgroup.IsIndividual = true;
            }
            var user = _dbContext.Student.FirstOrDefault(student => student.Id == userId);
            newSubgroup.Students.Add(user);
            _dbContext.SubjectSubgroup.Add(newSubgroup);
            _dbContext.SaveChanges();
            
        }
        //! Zwraca podgrupy danej grupy
        public List<SubjectSubgroupGetDto> GetSubgroups(int groupId)
        {
            if(!(_dbContext.SubjectGroup.Any(group => group.Id == groupId)))
            {
                throw new NotFoundException("Wrong group id!");
            }

            var subgroups = _dbContext.SubjectSubgroup.Where(group => group.SubjectGroupId == groupId && group.IsIndividual == false).ToList();

            var groupTypeSubgroups = new List<SubjectSubgroupGetDto>();

            foreach (var subgroup in subgroups)
            {
                groupTypeSubgroups.Add(new SubjectSubgroupGetDto()
                {
                    Name = subgroup.Name,
                    Id = subgroup.Id,
                    SubjectGroupId = subgroup.SubjectGroupId,
                });
            }

            return groupTypeSubgroups;
        }
        //! Dodaje użytkownika do podgrupy
        public void AddUserToSubgroup(int subgroupId)
        {
            var userId = _userContextService.GetUserId;

            if (!(_dbContext.SubjectSubgroup.Any(subgroup => subgroup.Id == subgroupId)))
            {
                throw new NotFoundException("Wrong subgroup id!");
            }

            var subgroup = _dbContext.SubjectSubgroup
                .Include(subjectSubgroup => subjectSubgroup.Students)
                .FirstOrDefault(subgroup => subgroup.Id == subgroupId && subgroup.IsIndividual == false);
            var user = _dbContext.Student.FirstOrDefault(student => student.Id == userId);

            subgroup.Students.Add(user);
            _dbContext.SubjectSubgroup.Update(subgroup);

            _dbContext.SaveChanges();
        }
    }
}
