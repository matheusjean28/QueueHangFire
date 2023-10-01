using Hangfire;



using Hangfire.Storage.SQLite;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationServiceServices
{

    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
    {
        services.AddHangfire(configuration => configuration
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage("connectionString")); 

        services.AddHangfireServer();
    }

    }

}