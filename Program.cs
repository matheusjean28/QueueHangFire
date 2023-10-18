using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DeviceContext;
using MainDatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReaderCsvProcess", Version = "v1" });
});
builder.Services.AddDbContext<DeviceDb>(opt => opt.UseSqlite("Data Source=C:\\dev\\database\\Mac.db"));
builder.Services.AddDbContext<MainDatabase>(opt => opt.UseSqlite("Data Source=C:\\dev\\database\\MainDatabase.db"));
builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); 
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage("Data Source=C:\\dev\\database\\Workers.db"));

builder.Services.AddHangfireServer();
builder.Services.AddControllers();

builder.Services.AddHangfire(configuration => configuration
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage());

var app = builder.Build();
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});



app.MapHangfireDashboard("/hangfire", new DashboardOptions
{
});
app.UseHangfireDashboard();

app.UseRouting();
app.MapControllers();
app.UseRouting();


app.Run();
