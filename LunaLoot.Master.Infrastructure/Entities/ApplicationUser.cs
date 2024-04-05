using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LunaLoot.Master.Infrastructure.Entities;

public class ApplicationUser: IdentityUser<Guid>
{
  
    
    
    protected void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}