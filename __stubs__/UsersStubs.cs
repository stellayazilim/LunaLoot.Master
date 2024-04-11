using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace __stubs__;

public class UsersStubs
{
    public static List<ApplicationUser> UserStubs()
    {

        var passwordHasher = new PasswordHasher<ApplicationUser>();
        return new List<ApplicationUser>
        {
            new ApplicationUser
            {
                FirstName = "jhon",
                LastName = "doe",
                Email = "jhon@doe.com",
                RefreshTokens = new string[]{},
                MobilePhoneNumber =  "12345",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "1234")

            },
            new ApplicationUser
            {
                FirstName = "jhon1",
                LastName = "doe1",
                Email = "jhon1@doe.com",
                RefreshTokens = new string[]{},
                MobilePhoneNumber =  "12345",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "1234")

            }
        };
    }
}