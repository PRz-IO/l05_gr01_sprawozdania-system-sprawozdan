using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.DbModels;
using SystemSprawozdan.Backend.Services;
using SystemSprawozdan.Shared.Dto;
using SystemSprawozdan.Shared.Enums;


namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class MajorController : ControllerBase
    {
        public readonly IMajorService _majorService;

        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRoleEnum.Teacher))]
        public ActionResult<IEnumerable<MajorGetDto>> GetMajors()
        {
            var result = _majorService.GetMajors();
            return Ok(result);
        }
    }
    
}
