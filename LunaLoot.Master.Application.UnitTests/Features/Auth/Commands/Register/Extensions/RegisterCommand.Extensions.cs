using FluentAssertions;
using LunaLoot.Master.Application.Auth.Commands.Register;

namespace LunaLoot.Master.Application.UnitTests.Features.Auth.Commands.Register.Extensions;

public static class RegisterCommandExtensions
{
    public static void AssetCommandFrom(this RegisterCommand command)
    {
        command.FirstName.Should().NotBeNullOrEmpty();
        command.LastName.Should().NotBeNullOrEmpty();
        command.Email.Should().NotBeNullOrEmpty();
        command.MobilePhoneNumber.Should().NotBeNullOrEmpty();
        command.Password.Should().NotBeNullOrEmpty();
    }
}