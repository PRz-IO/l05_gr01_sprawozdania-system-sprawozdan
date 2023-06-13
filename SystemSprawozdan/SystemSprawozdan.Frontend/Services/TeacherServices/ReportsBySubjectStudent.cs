using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SystemSprawozdan.Frontend.Services;
using SystemSprawozdan.Shared.Dto.ReportTopic;

public interface IReportTopicPageService
{
    Task<List<ReportTopicForStudentGetDto>> GetSubmittedReportsByStudentAndSubject(int studentId, int subjectId);
}

public class ReportTopicPageService : IReportTopicPageService
{
    private readonly IAppHttpClient appHttpClient;

    public ReportTopicPageService(IAppHttpClient appHttpClient)
    {
        this.appHttpClient = appHttpClient;
    }

    public async Task<List<ReportTopicForStudentGetDto>> GetSubmittedReportsByStudentAndSubject(int studentId, int subjectId)
    {
        var url = $"/api/ReportTopic/ForTeacher?studentId={studentId}&subjectId={subjectId}";
        var response = await appHttpClient.Get<List<ReportTopicForStudentGetDto>>(url);
        return response;
    }
}