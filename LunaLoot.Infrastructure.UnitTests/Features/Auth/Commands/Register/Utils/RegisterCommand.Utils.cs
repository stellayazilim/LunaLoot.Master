using LunaLoot.Infrastructure.UnitTests.Utils.Constants;
using LunaLoot.Master.Application.Auth.Commands.Register;

namespace LunaLoot.Infrastructure.UnitTests.Features.Auth.Commands.Register.Utils;

public static class RegisterCommandUtils
{
    public static RegisterCommand CreateCommand (int? fixture) =>
        new RegisterCommand(
            FirstName: fixture is not null ? $"{Constants.Auth.FirstName}_{fixture}" : Constants.Auth.FirstName,
            LastName: fixture is not null ? $"{Constants.Auth.LastName}_{fixture}":Constants.Auth.LastName,
            Email: fixture is not null ? $"{fixture}__{Constants.Auth.Email}":Constants.Auth.Email,
            MobilePhoneNumber:fixture is not null ? $"{Constants.Auth.MobilePhoneNumber}_{fixture}": Constants.Auth.MobilePhoneNumber,
            Password:fixture is not null ? $"{Constants.Auth.Password}_{fixture}":Constants.Auth.Password
        );

    public static IEnumerable<RegisterCommand> CreateCommandInRange(int range) =>
        Enumerable.Range(0, range).Select(x =>
            CreateCommand(x)).ToList();
}