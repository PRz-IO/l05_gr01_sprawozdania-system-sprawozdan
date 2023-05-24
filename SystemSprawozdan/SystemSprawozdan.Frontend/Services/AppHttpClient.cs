using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using SystemSprawozdan.Frontend.CustomClasses;

namespace SystemSprawozdan.Frontend.Services
{
    public interface IAppHttpClient
    {
        public Task<TResponse?> Get<TResponse>(string url, HttpParameter parameter);
        public Task<TResponse?> Get<TResponse>(string url, List<HttpParameter>? parameters = null);
        public Task<string> PostFormData(string url, HttpContent body, HttpParameter parameter);

        public Task<string> PostFormData(string url, HttpContent body, List<HttpParameter>? parameters = null);
        public Task<string> Post<TBody>(string url, TBody body, HttpParameter parameter);
        public Task<string> Post<TBody>(string url, TBody body, List<HttpParameter>? parameters = null);
        public Task<string> Put<TBody>(string url, TBody body, HttpParameter parameter);

        public Task<string> Put<TBody>(string url, TBody body, List<HttpParameter>? parameters = null);
        public Task DownloadFile(string url, string fileName = "report");
        public T? SerializeStringToObject<T>(string json);
    }

    public class AppHttpClient : IAppHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializer = new (){ PropertyNameCaseInsensitive = true };
        private readonly IJSRuntime _js;

        public AppHttpClient(HttpClient httpClient, HttpInterceptorService interceptor, IJSRuntime js)
        {
            interceptor.RegisterEvent();
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7184/api/");
            _js = js;
        }

        public async Task<TResponse?> Get<TResponse>(string url, HttpParameter? parameter = null)
        {
            var parameters = new List<HttpParameter>();
            parameters.Add(parameter);
            return await Get<TResponse>(url, parameters);
        }

        public async Task<TResponse?> Get<TResponse>(string url, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            
            return SerializeStringToObject<TResponse>(content);
        }
        public async Task<string> Post<TBody>(string url, TBody body, HttpParameter? parameter = null)
        {
            var parameters = new List<HttpParameter>();
            parameters.Add(parameter);
            return await Post<TBody>(url, body, parameters);
        }

        public async Task<string> Post<TBody>(string url, TBody body, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PostAsJsonAsync(url, body);
            
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> PostFormData(string url, HttpContent body, HttpParameter? parameter = null)
        {
            var parameters = new List<HttpParameter>();
            parameters.Add(parameter);
            return await PostFormData(url, body, parameters);
        }

        public async Task<string> PostFormData(string url, HttpContent body, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PostAsync(url, body);
            
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> Put<TBody>(string url, TBody body, HttpParameter? parameter = null)
        {
            var parameters = new List<HttpParameter>();
            parameters.Add(parameter);
            return await Put<TBody>(url, body, parameters);
        }

        public async Task<string> Put<TBody>(string url, TBody body, List<HttpParameter>? parameters)
        {
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PutAsJsonAsync(url, body);
            
            return await response.Content.ReadAsStringAsync();
        }

        public async Task DownloadFile(string url, string fileName)
        {
            var response = await _httpClient.GetAsync(url);
            var fileStream = response.Content.ReadAsStream();
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await _js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef); // nazwa funkcji JS w Index.html
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
