using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Exceptions;

namespace SystemSprawozdan.Backend.Services
{
    public interface ISubjectSubgroupService
    {
        void CreateSubgroup(CreateSubgroupDto createSubgroupDto);
        List<GroupTypeSubjectSubgroupDto> GetSubgroups(int GroupId);
        void AddUserToSubgroup(int SubgroupId);
    }

    public class SubjectSubgroupService : ISubjectSubgroupService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public SubjectSubgroupService(ApiDbContext dbContext, 
            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        //metodki
        public void CreateSubgroup(CreateSubgroupDto createSubgroupDto)
        {
            var userId = _userContextService.GetUserId;

            var newSubgroup = new SubjectSubgroup()
            {
                SubjectGroupId = createSubgroupDto.SubjectGroupId,
                Students = new List<Student>(),
            };

            if (!createSubgroupDto.isIndividual)
            {
                newSubgroup.Name = createSubgroupDto.SubgroupName;
            }
            else
            {
                newSubgroup.Name = null;
            }
            var user = _dbContext.Student.FirstOrDefault(student => student.Id == userId);
            newSubgroup.Students.Add(user);
            _dbContext.SubjectSubgroup.Add(newSubgroup);
            _dbContext.SaveChanges();
            
        }
        public List<GroupTypeSubjectSubgroupDto> GetSubgroups(int GroupId)
        {
            var subgroups = _dbContext.SubjectSubgroup.Where(group => group.SubjectGroupId == GroupId && group.Name != null).ToList();

            var groupTypeSubgroups = new List<GroupTypeSubjectSubgroupDto>();

            foreach (var subgroup in subgroups)
            {
                groupTypeSubgroups.Add(new GroupTypeSubjectSubgroupDto()
                {
                    Name = subgroup.Name,
                    Id = subgroup.Id,
                    SubjectGroupId = subgroup.SubjectGroupId,
                });
            }

            return groupTypeSubgroups;
        }
        public void AddUserToSubgroup(int SubgroupId)
        {// tylko studenci - forbid
            var userId = _userContextService.GetUserId;
            var subgroup = _dbContext.SubjectSubgroup
                .Include(subjectSubgroup => subjectSubgroup.Students)
                .FirstOrDefault(subgroup => subgroup.Id == SubgroupId && subgroup.Name != null);
            var user = _dbContext.Student.FirstOrDefault(student => student.Id == userId);
            if (subgroup == null)
                throw new BadRequestException("Wrong subgroup id!");
            else if (user == null)
                throw new ForbidException();
            else
            {
                subgroup.Students.Add(user);
                _dbContext.SubjectSubgroup.Update(subgroup);
            }
            _dbContext.SaveChanges();
        }
    }
}
