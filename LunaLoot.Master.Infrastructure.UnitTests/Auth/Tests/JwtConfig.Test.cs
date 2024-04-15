using FakeItEasy;
using LunaLoot.Master.Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Tests;

public class JwtConfig_Test
{

    [Fact]
    public void ShouldRegisterJwtConfig()
    {

        var services = A.Fake<IServiceCollection>();
        var config = A.Fake<IConfiguration>();

        //services.Setup(x => x.AddAuthentication(It.IsAny<Action<AuthenticationOptions>>())).Returns(It.IsAny< AuthenticationBuilder>());
        services.UseJWT(config);
    }
}