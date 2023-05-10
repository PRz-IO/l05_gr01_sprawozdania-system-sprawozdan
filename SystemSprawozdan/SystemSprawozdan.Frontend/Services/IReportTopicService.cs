using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services;

public interface IReportTopicService
{
    public void ConvertDto(ReportTopicGetDto reportTopicGetDto);
}

public class ReportTopicService : IReportTopicService
{
    public void ConvertDto(ReportTopicGetDto reportTopicGetDto)
    {
        ;
    }
}