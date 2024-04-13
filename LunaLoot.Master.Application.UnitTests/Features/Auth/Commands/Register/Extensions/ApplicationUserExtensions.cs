using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Xunit.Abstractions;

namespace LunaLoot.Infrastructure.UnitTests.Features.Auth.Commands.Register.Extensions;

public static class ApplicationUserExtensions
{
    
    public static bool CompareUserFrom(this ApplicationUser user, ApplicationUser other)
    {
        return user.FirstName == other.FirstName &&
               user.LastName == other.LastName &&
               user.Email == other.Email &&
               user.UserName == other.UserName &&
               user.PasswordHash == other.PasswordHash &&
               user.MobilePhoneNumber == other.MobilePhoneNumber;
    } 
}