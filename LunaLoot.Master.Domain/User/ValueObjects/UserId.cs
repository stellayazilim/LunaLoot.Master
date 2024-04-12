using LunaLoot.Master.Domain.Common.Primitives;

namespace LunaLoot.Master.Domain.User.ValueObjects;

public class UserId: ValueObject
{
    public Guid Value { get; }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create() => new UserId(Guid.NewGuid());
}

