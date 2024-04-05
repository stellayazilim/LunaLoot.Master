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
                Email = "jhondoe@example.com",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "1234")

            },
            new ApplicationUser
            {
                Email = "jhondoe2@example.com",
                PasswordHash = passwordHasher.HashPassword(new ApplicationUser(), "1234")

            }
        };
    }
}