using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services
{
    public interface IAuthService
    {
        public void Login(LoginUserDto loginUser);
        public void Register(RegisterStudentDto registerStudent);
        public void RestorePassword(RestoreUserPasswordDto restoreUserPassword);
    }
    public class AuthService : IAuthService
    {
        private readonly IAppHttpClient _httpClient;
        public AuthService(IAppHttpClient httpClient)
        {
            _httpClient = httpClient;
        }  

        public async void Login(LoginUserDto loginUser)
        {
            var response = await _httpClient.Post<string, LoginUserDto>("Account/login", loginUser);
            _httpClient.SetTokenValue(response);
        }

        public async void Register(RegisterStudentDto registerStudent)
        {
            await _httpClient.Post<int?, RegisterStudentDto>("Account/register", registerStudent);
        }

        public async void RestorePassword(RestoreUserPasswordDto restoreUserPassword)
        {
            await _httpClient.Put<int?, RestoreUserPasswordDto>("Account/restorePassword", restoreUserPassword);
        }

    }
}
