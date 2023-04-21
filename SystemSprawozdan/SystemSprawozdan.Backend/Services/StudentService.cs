using Microsoft.AspNetCore.Identity;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Services
{
    public interface IStudentService
    {
        string LoginUser(LoginUserDto dto);
    }

    public class StudentService : IStudentService
    {
        private readonly ApiDbContext _dbContext;

        public StudentService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string LoginUser(LoginUserDto loginUserDto)
        {
            return "Dzieki dziala";
        }
    }
}
