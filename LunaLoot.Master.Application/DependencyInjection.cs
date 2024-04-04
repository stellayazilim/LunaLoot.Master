using Microsoft.Extensions.DependencyInjection;
using LunaLoot.Master.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace LunaLoot.Master.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddLunaLootMasterApplication(this IServiceCollection services, ConfigurationManager config)
    {

        services.AddLunaLootMasterInfrastructure(config);
        return services;
    }
}