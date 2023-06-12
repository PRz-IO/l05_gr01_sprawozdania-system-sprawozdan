using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Services
{
     public interface ISubjectService 
    {
        public List<SubjectGetDto> GetSubjects();
        public List<TeacherSubjectGetDto> GetTeacherSubjects();
        public Subject AddSubject(SubjectPostDto subjectPostDto);
        public SubjectGetDto GetSubject(int subjectId);
    }
    public class SubjectService : ISubjectService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public SubjectService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        //! Pobieranie przedmiotów z bazy danych 
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

        public SubjectGetDto GetSubject(int subjectId)
        {
            var subject = _dbContext.Subject.Include(subject => subject.Major).FirstOrDefault(subject => subject.Id == subjectId);
            
            var subjectDto = new SubjectGetDto()
            {
                Id = subject.Id,
                Name = subject.Name,
                MajorCode = subject.Major.MajorCode,
                SubjectGroups = new List<SubjectGroupGetDto>()
            };  
            
            return subjectDto;
        }
        
        //! Pobieranie przedmiotów z bazy danych, do których należy prowadzący
        public List<TeacherSubjectGetDto> GetTeacherSubjects()
        {
            var teacherId = _userContextService.GetUserId;
            var subjects = _dbContext.Subject.Where(subject =>
                subject.SubjectGroups.Any(subjectGroup 
                    => subjectGroup.TeacherId == teacherId)).ToList();
            var subjectDtos = new List<TeacherSubjectGetDto>();
            foreach (var subject in subjects)
            {
                var temp = new TeacherSubjectGetDto()
                {
                    Id = subject.Id,
                    Name = subject.Name
                };
                subjectDtos.Add(temp);
            }
            return subjectDtos;
        }
        //! Dodawanie nowego przedmiotu
        public Subject AddSubject(SubjectPostDto subjectPostDto)
        {
            var major = _dbContext.Major.FirstOrDefault(major => major.MajorCode == subjectPostDto.MajorCode);
            var subjectToAdd = new Subject
            {
                Name = subjectPostDto.Name,
                Description = subjectPostDto.Description,
                Term = subjectPostDto.Term,
                MajorId = major.Id
            };

            _dbContext.Subject.Add(subjectToAdd);
            _dbContext.SaveChanges();

            return subjectToAdd;
        }
    }
}
