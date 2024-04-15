using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunaLoot.Master.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection 
        AddLunaLootMasterInfrastructure(
            this IServiceCollection services, 
            IConfiguration config)
    {

        string connectionString = config.GetConnectionString("DefaultConnection") ??
                                  throw new InvalidOperationException("Empty connection string");
        services.AddLogging();
        services.AddDbContext<LunaLootMasterDbContext>(optionsBuilder =>
        {
             optionsBuilder.UseSqlServer(
              connectionString
             );
        });
        services.UseIdentity(config);
        services.UseJWT(config);
        
        return services;
    }
}