using Blazored.SessionStorage;
using System.Net.Http.Json;
using SystemSprawozdan.Shared.Dto;

namespace SystemSprawozdan.Frontend.Pages.Auth.Services
{
    public interface IAuthService
    {
        public void Login(LoginUserDto loginUser);
    }
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _session;
        public AuthService(HttpClient httpClient, ISessionStorageService session)
        {
            _httpClient = httpClient;
            _session = session;
        }

        public async void Login(LoginUserDto loginUser)
        {
            var token = await GetToken(loginUser);
           _session.SetItemAsStringAsync("token", token.ToString());
        }

        private async Task<string> GetToken(LoginUserDto loginUser)
        {
            using var response = await _httpClient.PostAsJsonAsync("https://localhost:7184/api/Account/login", loginUser);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
