using System.Net;
using System.Runtime.Serialization;
using MatBlazor;
using Microsoft.AspNetCore.Components;
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
    private readonly NavigationManager _navManager;
    private readonly IMatToaster _toaster;

    public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager, IMatToaster toaster)
    {
        _interceptor = interceptor;
        _navManager = navManager;
        _toaster = toaster;
    }
    public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
    private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        if (!e.Response.IsSuccessStatusCode)
        {
            if(e.Response.StatusCode == HttpStatusCode.Unauthorized)
                _navManager.NavigateTo("/login");
            
            var message = await e.Response.Content.ReadAsStringAsync();
            _toaster.Add(message, MatToastType.Danger, "Error");
        }
    }
    public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
}