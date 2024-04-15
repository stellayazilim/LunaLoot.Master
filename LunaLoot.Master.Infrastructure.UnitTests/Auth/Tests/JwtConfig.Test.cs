using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using LunaLoot.Master.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Tests;

public static class MockExtension
{
    public static AuthenticationBuilder AddAuthentication(this IServiceCollection services,
        Action<AuthenticationOptions> configureOptions)
    {
        var ab = new AuthenticationBuilder(services);

        var fakeOpts = A.Fake<AuthenticationOptions>();
        
        configureOptions.Invoke(fakeOpts);

        fakeOpts.DefaultScheme.Should().Be(JwtBearerDefaults.AuthenticationScheme);
        return ab;
    }
}

public class JwtConfig_Test
{

    [Fact]
    public void ShouldRegisterJwtConfig()
    {
        
        

        var services = A.Fake<ServiceCollection>();

        var fa = A.Fake<AuthenticationBuilder>();
        
        
        var config = A.Fake<IConfiguration>();
        
        
      
        services.UseJWT(config);

        A.CallTo(() => services.AddAuthentication()).Returns(fa);
    }


}