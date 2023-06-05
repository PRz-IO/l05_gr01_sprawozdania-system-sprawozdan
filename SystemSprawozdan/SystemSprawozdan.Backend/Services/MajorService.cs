using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Exceptions;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Services
{
    public interface IMajorService
    {
        public List<MajorGetDto> GetMajors();
    }

    public class MajorService : IMajorService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        
        
        public MajorService(ApiDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public List<MajorGetDto> GetMajors()
        {
            var majors = _dbContext.Major.ToList();
            var majorDtos = new List<MajorGetDto>();
            foreach (var major in majors)
            {
                var tempMajorDto = new MajorGetDto
                {
                    Name = major.Name,
                    MajorCode = major.MajorCode
                };
                majorDtos.Add(tempMajorDto);    
            }

            return majorDtos;
        }
    }
    
    
    
}