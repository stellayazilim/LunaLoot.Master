using System.Reflection;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LunaLoot.Master.Infrastructure.Context;

public class LunaLootMasterDbContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
   
    public LunaLootMasterDbContext(DbContextOptions optionsBuilder): base(optionsBuilder)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<ApplicationUser>()
            .Property(e=>e.RefreshTokens)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            
        base.OnModelCreating(builder);
    }
}