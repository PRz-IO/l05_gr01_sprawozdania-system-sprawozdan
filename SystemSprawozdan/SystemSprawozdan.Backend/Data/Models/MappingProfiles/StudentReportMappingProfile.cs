using AutoMapper;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Data.Models.MappingProfiles;

public class StudentReportMappingProfile : Profile
{
    public StudentReportMappingProfile()
    {
        CreateMap<Student, StudentBasicGetDto>();

        CreateMap<StudentReport, StudentReportGetDto>()
            .ForMember(dest => dest.IsIndividual, opt => opt.MapFrom(src => src.SubjectSubgroup.IsIndividual))
            .ForPath(dest => dest.SubjectSubgroup.Students,opt => opt.MapFrom(src => src.SubjectSubgroup.Students.ToList()));
    }
    
}