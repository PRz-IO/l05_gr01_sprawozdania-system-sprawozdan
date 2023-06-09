using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Frontend.Shared;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles;

public class SubjectGroupMappingProfile : Profile
{
    public SubjectGroupMappingProfile()
    {
        CreateMap<SubjectGroup, SubjectGroupGetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.GroupType, opt => opt.MapFrom(src => src.GroupType))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name))
            .ForMember(dest => dest.TeacherSurname, opt => opt.MapFrom(src => src.Teacher.Surname))
            .ForMember(dest => dest.TeacherDegree, opt => opt.MapFrom(src => src.Teacher.Degree))
            .ForMember(dest => dest.MajorCode, opt => opt.MapFrom(src => src.Subject.Major.MajorCode));
    }
}