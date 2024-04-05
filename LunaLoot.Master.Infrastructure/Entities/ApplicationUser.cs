using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LunaLoot.Master.Infrastructure.Entities;

public class ApplicationUser: IdentityUser<Guid>, IEquatable<ApplicationUser>
{
  
    
    
    protected void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }

    public bool Equals(ApplicationUser? other)
    {
        return Id.Equals(other.Id);
    }
}