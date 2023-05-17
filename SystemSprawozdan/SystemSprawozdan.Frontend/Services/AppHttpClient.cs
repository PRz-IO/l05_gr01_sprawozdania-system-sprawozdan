using System.Text.Json;
using System.Net.Http.Json;
using SystemSprawozdan.Frontend.CustomClasses;

namespace SystemSprawozdan.Frontend.Services
{
    public interface IAppHttpClient
    {
        public Task<TResponse?> Get<TResponse>(string url, List<HttpParameter>? parameters = null);
        public Task<string> Post<TBody>(string url, TBody body, List<HttpParameter>? parameters = null);
        public Task<string> Put<TBody>(string url, TBody body, List<HttpParameter>? parameters = null);
        public T? SerializeStringToObject<T>(string json);
    }

    public class AppHttpClient : IAppHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializer = new (){ PropertyNameCaseInsensitive = true };

        public AppHttpClient(HttpClient httpClient, HttpInterceptorService interceptor)
        {
            interceptor.RegisterEvent();
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7184/api/");
        }
        
        public async Task<TResponse?> Get<TResponse>(string url, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            return SerializeStringToObject<TResponse>(content);
        }

        public async Task<string> Post<TBody>(string url, TBody body, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PostAsJsonAsync(url, body);
            
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Put<TBody>(string url, TBody body, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PutAsJsonAsync(url, body);
            
            return await response.Content.ReadAsStringAsync();
        }

        public T? SerializeStringToObject<T>(string json)
        {
            var jsonObject = JsonSerializer.Deserialize<T>(json, _serializer);
            return jsonObject;
        }

        private static string AddParamsToUrl(string url, List<HttpParameter>? parameters)
        {
            if (parameters is null) return url;
            
            foreach (var parameter in parameters)
            {
                url = parameter.AddToUrl(url);
            }

            return url;
        }
    }
}
