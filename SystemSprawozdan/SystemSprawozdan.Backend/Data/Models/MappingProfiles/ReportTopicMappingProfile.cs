using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles
{
    public class ReportTopicMappingProfile : Profile
    {
        public ReportTopicMappingProfile()
        {
            CreateMap<ReportTopic, ReportTopicGetDto>()
                .ForMember(dest => dest.ReportTopicName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ReportTopicDeadline, opt => opt.MapFrom(src => src.Deadline))
                .ForMember(dest => dest.SubjectGroupName, opt => opt.MapFrom(src => src.SubjectGroup.Name))
                .ForMember(dest => dest.SubjectGroupType, opt => opt.MapFrom(src => src.SubjectGroup.GroupType))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.SubjectGroup.Subject.Name))
                .ForMember(dest => dest.MajorName, opt => opt.MapFrom(src => src.SubjectGroup.Subject.Major.Name));
        }
    }
}
