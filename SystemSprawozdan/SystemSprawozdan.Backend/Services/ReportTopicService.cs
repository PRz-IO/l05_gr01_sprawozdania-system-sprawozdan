using SystemSprawozdan.Backend.Controllers;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Services
{
    public interface IReportTopicService
    {
        ReportTopicGetDto GetReportTopic(int reportTopicId);
    }

    public class ReportTopicService : IReportTopicService
    {
        private readonly ApiDbContext _dbContext;
        public ReportTopicService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public ReportTopicGetDto GetReportTopic(int reportTopicId)
        {
            var reportTopic = _dbContext.ReportTopic.FirstOrDefault(t => t.Id == reportTopicId);
            if (reportTopic is null) return null;
            var reportTopicDto = new ReportTopicGetDto()
            {
                Name = reportTopic.Name,
                Deadline = reportTopic.Deadline
            };

            return reportTopicDto;
        }
    }
}
