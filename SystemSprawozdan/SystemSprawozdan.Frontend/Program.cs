global using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MatBlazor;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using SystemSprawozdan.Frontend;
using SystemSprawozdan.Frontend.Providers;
using SystemSprawozdan.Frontend.Services;
using SystemSprawozdan.Frontend.Services.StudentServices;
using SystemSprawozdan.Frontend.Services.TeacherServices;

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
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.TopCenter;
    config.PreventDuplicates = false;
    config.NewestOnTop = true;
    config.ShowCloseButton = true;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 3000;
});


builder.Services.AddScoped<IAppHttpClient, AppHttpClient>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITReportHttpService, TReportHttpService>();
builder.Services.AddScoped<ISReportHttpService, SReportHttpService>();
builder.Services.AddScoped<IReportTopicFrontendService, ReportTopicFrontendService>();
builder.Services.AddScoped<IReportTopicPageService, ReportTopicPageService>();

await builder.Build().RunAsync();
