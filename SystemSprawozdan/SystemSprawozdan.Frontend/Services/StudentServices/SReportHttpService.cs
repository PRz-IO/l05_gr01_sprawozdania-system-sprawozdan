using Microsoft.AspNetCore.Components.Forms;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services.StudentServices;

public interface ISReportHttpService
{
    Task<StudentReportGetDto> GetStudentReport(int studentReportId);
    Task UploadFilesForStudentReport(int studentReportId, List<IBrowserFile> filesToSend);
    Task DownloadFilesFromStudentReport(StudentReportFileGetDto file);
    Task<int> PostStudentReport(StudentReportPostDto report);
    Task PutStudentReport(int studentReportId, StudentReportAsStudentPutDto reportAsStudent);
    Task<ReportTopicGetDto> GetReportTopic(int reportTopicId);
    Task<List<StudentReportFileGetDto>> GetStudentReportFiles(int studentReportId);
}

public class SReportHttpService : ISReportHttpService
{
    private readonly IAppHttpClient _httpClient;

    public SReportHttpService(IAppHttpClient appHttpClient)
    {
        _httpClient = appHttpClient;
    }

    public async Task<StudentReportGetDto> GetStudentReport(int studentReportId)
    {
        return await _httpClient.Get<StudentReportGetDto>($"StudentReport/fullReport/{studentReportId}");
    }

    public async Task<List<StudentReportFileGetDto>> GetStudentReportFiles(int studentReportId)
    {
        return await _httpClient.Get<List<StudentReportFileGetDto>>($"StudentReportFile/{studentReportId}");
    }

    public async Task<int> PostStudentReport(StudentReportPostDto report)
    {
        var response = await _httpClient.Post("StudentReport", report);
        return int.Parse(response);
    }

    public async Task PutStudentReport(int studentReportId, StudentReportAsStudentPutDto reportAsStudent)
    {
        await _httpClient.Put($"StudentReport/{studentReportId}/AsStudent", reportAsStudent);
    }

    public async Task DownloadFilesFromStudentReport(StudentReportFileGetDto file)
    {
        await _httpClient.DownloadFile($"StudentReportFile/download/{file.RandomizedFileName}", file.OriginalFileName);
    }

    public async Task UploadFilesForStudentReport(int studentReportId, List<IBrowserFile> files)
    {
        await _httpClient.UploadFiles($"StudentReportFile/{studentReportId}", files);
    }

    
    public async Task<ReportTopicGetDto> GetReportTopic(int reportTopicId)
    {
        return await _httpClient.Get<ReportTopicGetDto>($"ReportTopic/{reportTopicId}");
    }
}