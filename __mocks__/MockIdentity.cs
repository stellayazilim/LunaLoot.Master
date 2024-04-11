using LunaLoot.Master.Infrastructure.Entities;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace __mocks__;

public class MockIdentity
{
    
    public IServiceCollection services { get; }
    public MockIdentity()
    {

        var options = new DbContextOptionsBuilder<LunaLootMasterDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
       
        services = new ServiceCollection();

        services.AddDbContext<LunaLootMasterDbContext>(s => new DbContext(options));
        
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LunaLootMasterDbContext>()
            .AddSignInManager()

            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            //.AddUserValidator<ApplicationUser>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole<Guid>>();

        services.BuildServiceProvider();
    }
}