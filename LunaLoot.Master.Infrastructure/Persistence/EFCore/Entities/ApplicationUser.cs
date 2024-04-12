using Duende.IdentityServer.Models;
using LunaLoot.Master.Domain.User;
using LunaLoot.Master.Domain.User.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace LunaLoot.Master.Infrastructure.Entities;

public class ApplicationUser: IdentityUser<Guid>, IEquatable<ApplicationUser>
{
  
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string[] RefreshTokens { get; set; } 
    
    public string? MobilePhoneNumber { get; set; }
    
    public bool Equals(ApplicationUser? other)
    {
        return other != null && Id.Equals(other.Id);
    }
    

    public static implicit operator ApplicationUser(User user)
    {
        return new ApplicationUser
        {
            Id = user.Id.Value,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            MobilePhoneNumber = user.MobilePhoneNumber,
            UserName = user.Email,
            RefreshTokens = new string[]{}
            
        };
    }

    public  User AsUserAggregate()
    {
        return new User(new UserId(Id), FirstName, LastName, Email, PasswordHash, MobilePhoneNumber, new List<RoleId>());
    }
}