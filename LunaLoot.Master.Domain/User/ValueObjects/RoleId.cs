using LunaLoot.Master.Domain.Common.Primitives;

namespace LunaLoot.Master.Domain.User.ValueObjects;

public class RoleId: ValueObject
{
    public int Value { get; }

    public RoleId(int value)
    {
        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}