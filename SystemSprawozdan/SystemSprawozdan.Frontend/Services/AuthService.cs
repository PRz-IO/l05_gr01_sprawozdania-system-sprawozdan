using Microsoft.JSInterop;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Services
{
    public interface IAuthService
    {
        public Task Login(LoginUserDto loginUser);
        public void Register(RegisterStudentDto registerStudent);
        public void RestorePassword(RestoreUserPasswordDto restoreUserPassword);
    }
    public class AuthService : IAuthService
    {
        private readonly IAppHttpClient _httpClient;
        private readonly IJSRuntime _js;

        public AuthService(IJSRuntime js , IAppHttpClient httpClient)
        {
            _httpClient = httpClient;
            _js = js;
        }

        public async Task Login(LoginUserDto loginUser)
        {
            var token = await _httpClient.Post("Account/login", loginUser);
            await _js.InvokeVoidAsync("localStorage.setItem", "token", token);
        }

        public async void Register(RegisterStudentDto registerStudent)
        {
            await _httpClient.Post("Account/register", registerStudent);
        }

        public async void RestorePassword(RestoreUserPasswordDto restoreUserPassword)
        {
            await _httpClient.Put("Account/restorePassword", restoreUserPassword);
        }

    }
}
