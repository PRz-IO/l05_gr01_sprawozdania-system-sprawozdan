using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemSprawozdan.Backend.Data.Models.Dto;
using SystemSprawozdan.Backend.Services;

namespace SystemSprawozdan.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //TODO: Mikolaj: Logowanie
        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
        {
            var token = _studentService.LoginUser(loginUserDto);
            return Ok(token);
        }

        //TODO: Mikolaj: Rejestracja

        //TODO: Bartek: Trzeba zrobic GETa, który wyświetli podstawowe informacje o uzytkowniku

        //TODO: Bartek: Trzeba zrobić PUTa, którym będzie mozna zresetowac haslo
    }
}
