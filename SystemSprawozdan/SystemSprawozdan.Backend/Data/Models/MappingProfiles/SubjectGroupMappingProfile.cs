using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Data.Models.Dto;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles;

public class SubjectGroupMappingProfile : Profile
{
    public SubjectGroupMappingProfile()
    {
        CreateMap<SubjectGroup, SubjectGroupGetDto>()
            .ForMember(dest => dest.SubjectGroupId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
            .ForMember(dest => dest.MajorName, opt => opt.MapFrom(src => src.Subject.Major.Name))
            .ForMember(dest => dest.SubjectGroupGroupType, opt => opt.MapFrom(src => src.GroupType))
            .ForMember(dest => dest.SubjectGroupName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name));
    }
}