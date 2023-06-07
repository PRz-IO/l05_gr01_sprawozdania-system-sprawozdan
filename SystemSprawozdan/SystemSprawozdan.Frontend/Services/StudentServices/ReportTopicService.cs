using SystemSprawozdan.Frontend.CustomClasses;
using SystemSprawozdan.Frontend.Services;
using SystemSprawozdan.Shared.Dto.ReportTopic;

public interface IReportTopicFrontendService
{
    Task<List<ReportTopicForStudentGetDto>> GetReportTopicsForStudent(bool isSubmitted);
}

public class ReportTopicFrontendService: IReportTopicFrontendService
{
    private readonly IAppHttpClient httpClient;

    public ReportTopicFrontendService(IAppHttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<ReportTopicForStudentGetDto>> GetReportTopicsForStudent(bool isSubmitted)
    {
        var param = new HttpParameter(nameof(isSubmitted), isSubmitted);
        return await httpClient.Get<List<ReportTopicForStudentGetDto>>("ReportTopic/ForStudent", param);
    }
}