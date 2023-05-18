using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Services
{
    public interface IReportTopicService
    {
        ReportTopicGetDto GetReportTopic(int? reportTopicId, int? studentReportId);
    }

    public class ReportTopicService : IReportTopicService
    {
        private readonly ApiDbContext _dbContext;
        public ReportTopicService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ReportTopicGetDto GetReportTopic(int? reportTopicId, int? studentReportId)
        {
            var reportTopicDto = new ReportTopicGetDto();
            if (reportTopicId != null)
            {
                var reportTopic = _dbContext.ReportTopic.FirstOrDefault(t => t.Id == reportTopicId);
                if (reportTopic is null) return null;
                reportTopicDto.ReportTopicName = reportTopic.Name;
                reportTopicDto.ReportTopicDeadline = reportTopic.Deadline; 
                
            }

            if (studentReportId != null)
            {
                var studentReport = _dbContext.StudentReport.FirstOrDefault(report => report.Id == studentReportId);
                var reportTopic = _dbContext.ReportTopic.FirstOrDefault(topic => topic.Id == studentReport.ReportTopicId);

                reportTopicDto.ReportTopicName = reportTopic.Name;
                reportTopicDto.ReportTopicDeadline = reportTopic.Deadline;
            }

            return reportTopicDto;
        }
    }
}
