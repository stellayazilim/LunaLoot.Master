using LunaLoot.Infrastructure.UnitTests.Utils.Constants;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;

namespace LunaLoot.Infrastructure.UnitTests.Features.Auth.Utils;

public class AuthUtils
{
    public static IEnumerable<ApplicationUser> CreateUserRange(int range) =>
        Enumerable.Range(0, range).Select(index => CreateUser(index)).ToList();

    public static ApplicationUser CreateUser(int? suffix) {
        ApplicationUser user = new();

        user.FirstName = suffix is not null ? $"{Constants.Auth.FirstName}_{suffix}" : Constants.Auth.FirstName;
        user.LastName = suffix is not null ? $"{Constants.Auth.LastName}_{suffix}" : Constants.Auth.LastName;
        user.Email = suffix is not null ? $"{suffix}__{Constants.Auth.Email}" : Constants.Auth.Email;
        user.UserName = suffix is not null ? $"{suffix}__{Constants.Auth.Email}" : Constants.Auth.Email;
        user.MobilePhoneNumber = suffix is not null
            ? $"{Constants.Auth.MobilePhoneNumber}_{suffix}"
            : Constants.Auth.MobilePhoneNumber;
        user.PasswordHash = suffix is not null ? $"{Constants.Auth.Password}_{suffix}" : Constants.Auth.Password;

        return user;
    }
 
}