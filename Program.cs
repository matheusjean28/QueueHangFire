using Hangfire;
using HangfireAuthServices;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage("Workers.db"));

builder.Services.AddHangfireServer();
builder.Services.AddControllers();

builder.Services.AddHangfire(configuration => configuration
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage());

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.UseRouting();

app.MapHangfireDashboard("/hangfire", new DashboardOptions
{
});

app.Run();
