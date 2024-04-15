using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.UnitTests.Shared.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.CodeCoverage;
using Moq;
using Xunit.Sdk;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Tests;



public class JwtConfigTest
{


    
    [Fact]
    public void ShouldRegisterJwtConfig()
    {
        
        // arrange 
        
        var services = A.Fake<ServiceCollection>();
        var config = A.Fake<IConfiguration>();

        // act
        
        JwtConfigExtension.UseJWT(services, config);
        
        // assert
        // @todo find way to assert this exp
    }


}