using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class ReportCommentController : ControllerBase
    {
        private readonly IReportCommentService _reportCommentService;

        public ReportCommentController(IReportCommentService reportCommentService, ApiDbContext dbContext)
        {
            _reportCommentService = reportCommentService;
        }

        [HttpGet("{studentReportId}")]
        public ActionResult<ReportCommentGetDto> GetReportComment([FromRoute] int studentReportId)
        {
            var result = _reportCommentService.GetReportComment(studentReportId);
            return Ok(result);
        }
    }
}