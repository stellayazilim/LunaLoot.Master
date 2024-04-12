using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunaLoot.Master.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection 
        AddLunaLootMasterInfrastructure(
            this IServiceCollection services, 
            IConfiguration config)
    {
        services.AddDbContext<LunaLootMasterDbContext>(optionsBuilder =>
        {
             optionsBuilder.UseSqlServer(
                config.GetConnectionString("DefaultConnection") ?? 
                throw new InvalidOperationException("Empty connection string")
             );
        });
        services.UseIdentity(config);
        services.UseJWT(config);
        
        return services;
    }
}