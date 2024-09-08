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

        services.AddSingleton<ICardManager, CardManager>();
        services.AddSingleton<IBuildingManager, BuildingManager>();
        services.AddSingleton<IMapManager, MapManager>();
    }
}
