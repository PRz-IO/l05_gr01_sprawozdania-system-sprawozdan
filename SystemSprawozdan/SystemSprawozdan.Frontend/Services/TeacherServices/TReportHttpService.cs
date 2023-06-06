using SystemSprawozdan.Frontend.CustomClasses;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services.TeacherServices;

public interface ITReportHttpService
{
    Task<StudentReportGetDto> GetStudentReportDetails(int studentReportId);
    Task<List<StudentReportFileGetDto>> GetStudentReportFiles(int studentReportId);
    Task DownloadFileFromStudentReport(StudentReportFileGetDto file);
    Task<List<ReportTopicGetDto>?> GetReportTopics(bool? toCheck);
    Task<ReportTopicGetDto?> GetReportTopicById(int reportTopicId);
    Task<List<StudentReportGetDto>?> GetStudentReportsByReportTopicId(int reportTopicId, bool isIndividual, bool? toCheck);
    Task<List<StudentBasicGetDto>?> GetStudentWithoutReportByReportTopicId(int reportTopicId);
    Task PutStudentReport(int studentReportId, StudentReportAsTeacherPutDto ratedReport);
}

public class TReportHttpService : ITReportHttpService
{
    private readonly IAppHttpClient _httpClient;

    public TReportHttpService(IAppHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<StudentReportGetDto> GetStudentReportDetails(int studentReportId)
    {
        var studentReport = await _httpClient.Get<StudentReportGetDto>($"StudentReport/fullReport/{studentReportId}");
        return studentReport;
    }

    public async Task<List<StudentReportFileGetDto>> GetStudentReportFiles(int studentReportId)
    {
        var studentReportFiles = await _httpClient.Get<List<StudentReportFileGetDto>>($"StudentReportFile/{studentReportId}");
        return studentReportFiles;
    }

    public async Task PutStudentReport(int studentReportId, StudentReportAsTeacherPutDto ratedReport)
    {
        await _httpClient.Put($"StudentReport/{studentReportId}/AsTeacher", ratedReport);
    }

    public async Task DownloadFileFromStudentReport(StudentReportFileGetDto file)
    {
        await _httpClient.DownloadFile($"StudentReportFile/download/{file.RandomizedFileName}", file.OriginalFileName);
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

    public async Task<List<StudentReportGetDto>?> GetStudentReportsByReportTopicId(int reportTopicId, bool isIndividual, bool? toCheck)
    {
        List<HttpParameter> parameters = new()
        {
            new(nameof(isIndividual), isIndividual),
            new(nameof(toCheck), toCheck),
        };
        return await _httpClient.Get<List<StudentReportGetDto>>($"ReportTopic/{reportTopicId}/StudentReports", parameters);
    }

    public async Task<List<StudentBasicGetDto>?> GetStudentWithoutReportByReportTopicId(int reportTopicId)
    {
        return await _httpClient.Get<List<StudentBasicGetDto>>($"ReportTopic/{reportTopicId}/StudentReports/StudentsWithoutReport");
    }

}