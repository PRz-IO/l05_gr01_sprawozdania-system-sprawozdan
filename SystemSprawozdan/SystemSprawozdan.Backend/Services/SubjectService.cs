using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Services
{
     public interface ISubjectService 
    {
        public List<SubjectGetDto> GetSubjects();
    }
    public class SubjectService : ISubjectService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        public SubjectService(ApiDbContext dbContext, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;   
        }
        public List<SubjectGetDto> GetSubjects() 
        {
            var subjects = _dbContext.Subject.Include(subject => subject.Major).ToList();
            
            List<SubjectGetDto> allSubjects = new();

            foreach (var subject in subjects) 
            {
                allSubjects.Add(new SubjectGetDto()
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    MajorCode = subject.Major.MajorCode,
                    SubjectGroups = new List<SubjectGroupGetDto>()
                });    
            }
            return allSubjects;
        }
    }
}
