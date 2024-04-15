using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.UnitTests.Utils.Constants;

public static partial class Constants
{
    public const string Email = "jdoe@example.com";
    public const string InvalidEmail = "jdoeexample.com";
    public const string UserName = Email;
    public static IdentityErrorDescriber ErrorDescriber => new IdentityErrorDescriber();
}