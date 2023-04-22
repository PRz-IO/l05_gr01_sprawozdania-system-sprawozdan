using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Others;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Student, User>();
            CreateMap<Teacher, User>();
            CreateMap<Admin, User>();
        }
    }
}
