using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
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

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LunaLootMasterDbContext>()
            .AddSignInManager()

            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            //.AddUserValidator<ApplicationUser>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole<Guid>>();

        //services.AddTransient<IPasswordHasher<ApplicationUser>, PasswordHasher>();
        
        services.AddJwt(config);
        
        return services;
    }
}