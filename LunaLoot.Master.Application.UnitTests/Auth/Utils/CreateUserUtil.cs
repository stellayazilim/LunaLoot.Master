using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;

namespace LunaLoot.Master.Application.UnitTests.Auth.Utils;

public static class AuthUtils
{
    public static IEnumerable<ApplicationUser> CreateUserRange(int range) =>
        Enumerable.Range(0, range).Select(index => CreateUser(index)).ToList();

    public static ApplicationUser CreateUser(int? suffix) {
        ApplicationUser user = new();

        user.FirstName = suffix is not null ? $"{Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.FirstName}_{suffix}" : Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.FirstName;
        user.LastName = suffix is not null ? $"{Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.LastName}_{suffix}" : Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.LastName;
        user.Email = suffix is not null ? $"{suffix}__{Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.Email}" : Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.Email;
        user.UserName = suffix is not null ? $"{suffix}__{Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.Email}" : Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.Email;
        user.MobilePhoneNumber = suffix is not null
            ? $"{Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.MobilePhoneNumber}_{suffix}"
            : Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.MobilePhoneNumber;
        user.PasswordHash = suffix is not null ? $"{Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.Password}_{suffix}" : Master.Infrastructure.UnitTests.Auth.Constants.Constants.Auth.Password;

        return user;
    }
 
}