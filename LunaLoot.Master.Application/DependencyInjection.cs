using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using LunaLoot.Master.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace LunaLoot.Master.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddLunaLootMasterApplication(this IServiceCollection services, IConfiguration config)
    {

        services.AddLunaLootMasterInfrastructure(config);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}