using System.Net;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace SystemSprawozdan.Frontend.Services
{
    public class HttpParameter {
        public HttpParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }

        public string AddToUrl(string url)
        {
            if (!url.Contains("?"))
                return $"{url}?{Key}={Value}";
            
            if(url.Contains($"{Key}="))
            {
                var start = url.IndexOf($"{Key}=");
                var end = url.Substring(start).IndexOf("&");
                end = end == -1 ? url.Length - start : end;
                
                var replaceFragment = url.Substring(start, end);
                url = url.Replace(replaceFragment, $"{Key}={Value}");
                return url;
            }
            
            return $"{url}&{Key}={Value}";
        }
    }

    public interface IAppHttpClient
    {
        public Task<TResponse?> Get<TResponse>(string url, List<HttpParameter>? parameters = null);
        public Task<TResponse?> Post<TResponse, TBody>(string url, TBody body, List<HttpParameter>? parameters = null);
        public Task<TResponse?> Put<TResponse, TBody>(string url, TBody body, List<HttpParameter>? parameters = null);
        public void SetTokenValue(string token);
    }

    public class AppHttpClient : IAppHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;
        private readonly NavigationManager _navigator;
        private readonly JsonSerializerOptions _serializer = new (){ PropertyNameCaseInsensitive = true };

        public AppHttpClient(HttpClient httpClient, IJSRuntime js, NavigationManager navigator)
        {
            _navigator = navigator;
            _js = js;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7184/api/");
        }
        
        public async Task<TResponse?> Get<TResponse>(string url, List<HttpParameter>? parameters)
        {
            await SetAuthorizationHeader();
            url = AddParamsToUrl(url, parameters);
            var response = await _httpClient.GetAsync(url);
            
            return await SerializeContent<TResponse>(response);
        }

        public async Task<TResponse?> Post<TResponse, TBody>(string url, TBody body, List<HttpParameter>? parameters)
        {
            await SetAuthorizationHeader();
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PostAsJsonAsync(url, body);
            
            return await SerializeContent<TResponse>(response);
        }

        public async Task<TResponse?> Put<TResponse, TBody>(string url, TBody body, List<HttpParameter>? parameters)
        {
            await SetAuthorizationHeader();
            url = AddParamsToUrl(url, parameters);
            using var response = await _httpClient.PostAsJsonAsync(url, body);
            
            return await SerializeContent<TResponse>(response);
        }
        
        public async void SetTokenValue(string token)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", "token", token);
        }

        private async Task<T?> SerializeContent<T>(HttpResponseMessage response)
        {
            await HttpResponseHandler(response);
            
            var stringContent = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<T>(stringContent, _serializer );
                
            return content;
        }

        private async Task HttpResponseHandler(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    _navigator.NavigateTo("/login");
                    await _js.InvokeVoidAsync("localStorage.clear");
                    break;
                case HttpStatusCode.InternalServerError:
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    break;
            }
        }

        private async Task SetAuthorizationHeader()
        {
           try
           { 
               var token = await _js.InvokeAsync<string>("localStorage.getItem", "token");
               var auth = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
               _httpClient.DefaultRequestHeaders.Authorization = auth;
           }
           catch (Exception e)
           {
               _httpClient.DefaultRequestHeaders.Authorization =  null;
           }
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
