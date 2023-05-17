using System.Net;
using MatBlazor;
using Microsoft.JSInterop;
using Toolbelt.Blazor;

namespace SystemSprawozdan.Frontend.Services;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly AuthenticationStateProvider _authState;
    private readonly IMatToaster _toaster;
    private readonly IJSRuntime _js;

    public HttpInterceptorService(
        HttpClientInterceptor interceptor, 
        IMatToaster toaster, 
        AuthenticationStateProvider authState,
        IJSRuntime js
        )
    {
        _interceptor = interceptor;
        _authState = authState;
        _toaster = toaster;
        _js = js;
    }
    public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
    private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        if (e.Response.IsSuccessStatusCode) return;
        
        var message = await e.Response.Content.ReadAsStringAsync();
        _toaster.Add(message, MatToastType.Danger, "Error");
        Console.WriteLine(message);

        if (e.Response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "token");
            await _authState.GetAuthenticationStateAsync();
        }

    }
    public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
}