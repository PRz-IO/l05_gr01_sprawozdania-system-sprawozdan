using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using SystemSprawozdan.Backend.Data;
using SystemSprawozdan.Backend.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionString"];

// Add services to the container.
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApiDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IReportTopicService, ReportTopicService>();
builder.Services.AddScoped<IStudentReportService, StudentReportService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectGroupService, SubjectGroupService>();
builder.Services.AddScoped<ISubjectSubgroupService, SubjectSubgroupService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
