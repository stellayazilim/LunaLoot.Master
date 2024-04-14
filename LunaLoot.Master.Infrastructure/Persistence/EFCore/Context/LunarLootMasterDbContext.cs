using System.Reflection;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;

public class LunaLootMasterDbContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public LunaLootMasterDbContext(DbContextOptions optionsBuilder): base(optionsBuilder)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}