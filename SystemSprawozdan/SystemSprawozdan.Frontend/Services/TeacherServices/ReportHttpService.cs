using SystemSprawozdan.Frontend.CustomClasses;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services.TeacherServices;

public interface IReportHttpService
{
    Task<List<ReportTopicGetDto>?> GetReportTopics(bool? toCheck);
}

public class ReportHttpService : IReportHttpService
{
    private readonly IAppHttpClient _httpClient;

    public ReportHttpService(IAppHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ReportTopicGetDto>?> GetReportTopics(bool? toCheck)
    {
        var toCheckParam = new HttpParameter("toCheck", toCheck);
        return await _httpClient.Get<List<ReportTopicGetDto>>("/StudentReport", toCheckParam);
    }

}