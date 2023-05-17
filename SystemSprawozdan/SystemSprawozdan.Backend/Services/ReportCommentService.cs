using Microsoft.AspNetCore.Authorization;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Shared.Dto;


namespace SystemSprawozdan.Backend.Services
{
    public interface IReportCommentService
    {
        public ReportCommentGetDto GetReportComment(int studentReportId);
    }

    public class ReportCommentService : IReportCommentService
    {
        private readonly ApiDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;


        public ReportCommentService(ApiDbContext dbContext, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public ReportCommentGetDto GetReportComment(int studentReportId)
        {
            var reportComment =  
                _dbContext.ReportComment.FirstOrDefault(reportComment => reportComment.StudentReportId == studentReportId 
                                                                         && reportComment.StudentId == null);
            // na ten moment ReportComment może posiadać id-ka zarówno studenta i prowadzącego, dlatego borę taki ReportComment, który nie ma StudentId (żeby był to ten comment napisany przez prowadzącego)
            // TODO Logika działania komentarzy do przemyślenia i prawdopodobnie do zmiany.
            if (reportComment == null)
            {
                return new ReportCommentGetDto
                {
                    Content = String.Empty
                };
            }

            return new ReportCommentGetDto()
            {
                Content = reportComment.Content
            };
        }
    }
    
    
}