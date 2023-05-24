using SystemSprawozdan.Frontend.CustomClasses;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services.TeacherServices;

public interface ITReportHttpService
{
    Task<List<ReportTopicGetDto>?> GetReportTopics(bool? toCheck);
    Task<ReportTopicGetDto?> GetReportTopicById(int reportTopicId);
    Task<List<StudentReportGetDto>?> GetStudentReportsByReportTopicId(int reportTopicId, bool isIndividual, bool? isMarked);
}

public class TReportHttpService : ITReportHttpService
{
    private readonly IAppHttpClient _httpClient;

    public TReportHttpService(IAppHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ReportTopicGetDto>?> GetReportTopics(bool? toCheck)
    {
        var toCheckParam = new HttpParameter("toCheck", toCheck);
        return await _httpClient.Get<List<ReportTopicGetDto>>("ReportTopic", toCheckParam);
    }

    public async Task<ReportTopicGetDto?> GetReportTopicById(int reportTopicId)
    {
        return await _httpClient.Get<ReportTopicGetDto>($"ReportTopic/{reportTopicId}");
    }

    public async Task<List<StudentReportGetDto>?> GetStudentReportsByReportTopicId(int reportTopicId, bool isIndividual, bool? isMarked)
    {
        List<HttpParameter> parameters = new()
        {
            new(nameof(isIndividual), isIndividual),
            new(nameof(isMarked), isMarked),
        };
        return await _httpClient.Get<List<StudentReportGetDto>>($"ReportTopic/{reportTopicId}/StudentReports", parameters);
    }

}