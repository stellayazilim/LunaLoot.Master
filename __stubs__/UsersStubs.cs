using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace __stubs__;

public static class UsersStubs
{
    private static readonly PasswordHasher<ApplicationUser> PasswordHasher = new PasswordHasher<ApplicationUser>();
    public static List<ApplicationUser> UserStubs()
    {

        return new List<ApplicationUser>
        {
            new ApplicationUser
            {
                FirstName = "jhon",
                LastName = "doe",
                Email = "jhon@doe.com",
                RefreshTokens = new string[]{},
                MobilePhoneNumber =  "12345",
                PasswordHash = PasswordHasher.HashPassword(new ApplicationUser(), "1234")

            },
            new ApplicationUser
            {
                FirstName = "jhon1",
                LastName = "doe1",
                Email = "jhon1@doe.com",
                RefreshTokens = new string[]{},
                MobilePhoneNumber =  "12345",
                PasswordHash = PasswordHasher.HashPassword(new ApplicationUser(), "1234")

            }
        };
    }

    public static ApplicationUser UserStub => new()
    {
        FirstName = "jhon1",
        LastName = "doe1",
        Email = "jhon1@doe.com",
        RefreshTokens = new string[]{},
        MobilePhoneNumber =  "12345",
        PasswordHash = PasswordHasher.HashPassword(new ApplicationUser(), "1234")
    };
}