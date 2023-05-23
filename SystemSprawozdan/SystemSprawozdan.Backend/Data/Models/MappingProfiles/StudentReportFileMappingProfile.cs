using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles;

public class StudentReportFileMappingProfile : Profile
{
    public StudentReportFileMappingProfile()
    {
        CreateMap<StudentReportFile, StudentReportFileGetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RandomizedFileName, opt => opt.MapFrom(src => src.StoredFileName))
            .ForMember(dest => dest.OriginalFileName, opt => opt.MapFrom(src => src.FileName))
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType))
            .ForMember(dest => dest.StudentReportId, opt => opt.MapFrom(src => src.StudentReportId));
    }
}