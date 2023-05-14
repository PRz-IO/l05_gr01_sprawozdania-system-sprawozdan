using System.Net;
using System.Runtime.Serialization;
using MatBlazor;
using Toolbelt.Blazor;

namespace SystemSprawozdan.Frontend.Services;

[Serializable]
internal class HttpResponseException : Exception
{
    public HttpResponseException()
    {
    }
    public HttpResponseException(string message) 
        : base(message)
    {
    }
    public HttpResponseException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
    protected HttpResponseException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly AuthenticationStateProvider _authState;
    private readonly IMatToaster _toaster;

    public HttpInterceptorService(HttpClientInterceptor interceptor, IMatToaster toaster, AuthenticationStateProvider authState)
    {
        _interceptor = interceptor;
        _authState = authState;
        _toaster = toaster;
    }
    public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
    private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        if (e.Response.IsSuccessStatusCode) return;
        
        var message = await e.Response.Content.ReadAsStringAsync();
        _toaster.Add(message, MatToastType.Danger, "Error");

        if (e.Response.StatusCode == HttpStatusCode.Unauthorized)
            await _authState.GetAuthenticationStateAsync();
        
    }
    public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
}