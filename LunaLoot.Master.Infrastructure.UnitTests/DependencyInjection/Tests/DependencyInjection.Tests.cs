
using System.Configuration;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.AutoMock;

namespace LunaLoot.Master.Infrastructure.UnitTests.DependencyInjection.Tests;

public class DependencyInjectionTests
{

    private IServiceCollection _serviceCollection = new ServiceCollection();
    

    [Fact]
    public void TestDependencyInjection_WhenInvokingAddInfrastructure_ShouldRegisterServices()
    {
        // arrange
        var services = A.Fake<IServiceCollection>();
        var config = A.Fake<IConfiguration>();
        // act
        services.AddLunaLootMasterInfrastructure(config);
        // assert
        // @todo find way to assert this exp
    }

    
}