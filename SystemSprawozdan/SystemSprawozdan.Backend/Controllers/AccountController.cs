using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //TODO: Mikolaj: Logowanie
        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var token = _accountService.LoginUser(loginUserDto);
            return Ok(token);
        }

        //TODO: Mikolaj: Rejestracja
        [HttpPost("register")]
        public ActionResult RegisterStudent([FromBody] RegisterStudentDto registerUserDto)
        {
            _accountService.RegisterStudent(registerUserDto);
            return Ok();
        }

        [HttpPost("create")]
        public ActionResult RegisterTeacherOrAdmin([FromBody] RegisterTeacherOrAdminDto registerTeacherOrAdminDto)
        {
            _accountService.RegisterTeacherOrAdmin(registerTeacherOrAdminDto);
            return Ok();
        }

        //TODO: Bartek: Trzeba zrobic GETa, który wyświetli podstawowe informacje o uzytkowniku

        //TODO: Bartek: Trzeba zrobić PUTa, którym będzie mozna zresetowac haslo
    }
}
