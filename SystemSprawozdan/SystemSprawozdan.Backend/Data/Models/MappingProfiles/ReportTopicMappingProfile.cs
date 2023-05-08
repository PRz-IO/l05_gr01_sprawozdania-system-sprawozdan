using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles
{
    public class ReportTopicMappingProfile : Profile
    {
        public ReportTopicMappingProfile()
        {
            CreateMap<ReportTopic, ReportTopicDto>();
            CreateMap<SubjectGroup, ReportTopicDto>();
            CreateMap<Subject, ReportTopicDto>();
            CreateMap<Major, ReportTopicDto>();
        }
    }
}
