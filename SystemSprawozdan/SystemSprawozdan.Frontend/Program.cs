using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MatBlazor;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using SystemSprawozdan.Frontend;
using SystemSprawozdan.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var http = new HttpClient(){
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    };
    http.EnableIntercept(sp);
    return http;
});
builder.Services.AddHttpClientInterceptor();
builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.TopCenter;
    config.PreventDuplicates = false;
    config.NewestOnTop = true;
    config.ShowCloseButton = true;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 3000;
});

builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddScoped<IAppHttpClient, AppHttpClient>();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
