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
app.UseHangfireDashboard();

app.UseRouting();
app.MapControllers();
app.UseRouting();

app.MapHangfireDashboard("/hangfire", new DashboardOptions
{
});


var jobId = BackgroundJob.Enqueue(
    () => Console.WriteLine("Job Fire-and-forget!"));


var jobId2 = BackgroundJob.Enqueue(() => Console.WriteLine("Job fire-and-forget pai!"));

BackgroundJob.ContinueJobWith(
    jobId,
    () => Console.WriteLine($"Job continuation! (Job pai: {jobId})"));

app.Run();
