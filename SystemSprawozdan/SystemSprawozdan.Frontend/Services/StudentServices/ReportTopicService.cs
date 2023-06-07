using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Dto.ReportTopic;

public interface IReportTopicFrontendService
{
    Task<List<ReportTopicForStudentGetDto>> GetReportTopicsForStudent(bool isSubmitted);
}

public class ReportTopicFrontendService: IReportTopicFrontendService
{
    private readonly HttpClient httpClient;

    public ReportTopicFrontendService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<ReportTopicForStudentGetDto>> GetReportTopicsForStudent(bool isSubmitted)
    {
        string apiUrl = isSubmitted ? "api/ReportTopic/ForStudent?isSubmitted=true" : "api/ReportTopic/ForStudent";
        return await httpClient.GetFromJsonAsync<List<ReportTopicForStudentGetDto>>(apiUrl);
    }
}