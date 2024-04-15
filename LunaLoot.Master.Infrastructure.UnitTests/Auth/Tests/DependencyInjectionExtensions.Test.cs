using FakeItEasy;
using LunaLoot.Master.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Tests;

public class DependencyInjectionExtensionsTest
{


    [Fact]
    public void TestUseIdentity_WhenRegisterExtension_ShouldRegister()
    {
        // arrange
        var services = A.Fake<IServiceCollection>();
        var config = A.Fake<IConfiguration>();
        var builder = A.Fake<IdentityBuilder>();
        // act


        services.UseIdentity(config);


    }
}