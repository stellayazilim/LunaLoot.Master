using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using LunaLoot.Master.Infrastructure.UnitTests.Auth.Constants;
namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.AuthUtils;

public class AuthUtils
{
    public static IEnumerable<ApplicationUser> CreateUserRange(int range) =>
        Enumerable.Range(0, range).Select(index => CreateUser(index)).ToList();

    public static ApplicationUser CreateUser(int? suffix) {
        ApplicationUser user = new();

        user.FirstName = suffix is not null ? $"{Constants.Constants.Auth.FirstName}_{suffix}" : Constants.Constants.Auth.FirstName;
        user.LastName = suffix is not null ? $"{Constants.Constants.Auth.LastName}_{suffix}" : Constants.Constants.Auth.LastName;
        user.Email = suffix is not null ? $"{suffix}__{Constants.Constants.Auth.Email}" : Constants.Constants.Auth.Email;
        user.UserName = suffix is not null ? $"{suffix}__{Constants.Constants.Auth.Email}" : Constants.Constants.Auth.Email;
        user.MobilePhoneNumber = suffix is not null
            ? $"{Constants.Constants.Auth.MobilePhoneNumber}_{suffix}"
            : Constants.Constants.Auth.MobilePhoneNumber;
        user.PasswordHash = suffix is not null ? $"{Constants.Constants.Auth.Password}_{suffix}" : Constants.Constants.Auth.Password;

        return user;
    }
 
}