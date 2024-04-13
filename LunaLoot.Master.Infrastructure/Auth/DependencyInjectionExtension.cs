using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunaLoot.Master.Infrastructure.Auth;

public static class DependencyInjectionExtension
{
    public static IServiceCollection UseIdentity(this IServiceCollection services, IConfiguration config)
    {
        
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LunaLootMasterDbContext>()
            
            
            .AddSignInManager()
            // @todo add PasswordValidator 
            //.AddPasswordValidator<ApplicationUser>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddPasswordValidator<ApplicationPasswordValidator>()
            .AddDefaultTokenProviders()
            .AddUserValidator<ApplicationUserValidator>()
            .AddRoles<IdentityRole<Guid>>();
        services.AddTransient<IPasswordHasher<ApplicationUser>, ApplicationPasswordHasher>();
        return services;
    }
}