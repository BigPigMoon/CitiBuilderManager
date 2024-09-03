using CitiBuilderManager.Interfaces;
using CitiBuilderManager.Services;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace CitiBuilderManager.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureServices(this Application app, IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddNLog();
        });

        services.AddSingleton<ICardSpawner, CardSpawner>();
    }
}
