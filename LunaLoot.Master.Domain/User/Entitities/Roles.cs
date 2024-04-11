using System.Security.Claims;
using LunaLoot.Master.Domain.Common.Primitives;
using LunaLoot.Master.Domain.User.ValueObjects;

namespace LunaLoot.Master.Domain.User.Entitities;

public class Role: Entity<RoleId?>
{
    public string Name { get; }
    private readonly List<Claim> _claims;
    public IReadOnlyList<Claim> Claims => _claims.AsReadOnly();
    
    public Role(
        RoleId? id, 
        string name,
        List<Claim> claims) : base(id)
    {
        Name = name;
        _claims = claims;

    }


    public static Role Create(
        string name, List<Claim> claims) => new Role(null, name, claims);

}