using System.Security.Claims;
using LunaLoot.Master.Domain.Common.Primitives;
using LunaLoot.Master.Domain.User.ValueObjects;

namespace LunaLoot.Master.Domain.User;

public class User: AggregateRoot<UserId>
{
    public User(
        UserId id,
        string firstName,
        string lastName,
        string email, 
        string passwordHash,
        string mobilePhoneNumber,
        List<RoleId> roles) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        _roles = roles;
        MobilePhoneNumber = mobilePhoneNumber;
    }

    private readonly List<RoleId> _roles;

    public IReadOnlyList<RoleId> Roles => _roles.AsReadOnly();
    
    public string FirstName { get; }
    
    public string LastName { get; }
    
    public string Email { get; }
    
  
    
    public string PasswordHash { get; set; }
    

    public string MobilePhoneNumber { get;  }
    
    
}