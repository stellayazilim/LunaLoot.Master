using System.Data.OleDb;
using System.Reflection;
using FakeItEasy;

using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NSubstitute;


namespace LunaLoot.Master.Infrastructure.UnitTests.DependencyInjection.Tests;

public class DependencyInjectionTests
{

    private IServiceCollection _serviceCollection = new ServiceCollection();
    

    [Fact]
    public void TestDependencyInjection_WhenInvokingAddInfrastructure_ShouldRegisterServices()
    {
        // arrange
        var config = Substitute.For<IConfiguration>();
        var services = Substitute.For<IServiceCollection>();
        
        // act 
        services.AddLunaLootMasterInfrastructure(config);
        
        // assert

        services.Received().UseIdentity(config);
        config.Received().GetConnectionString("DefaultConnection");
    }

    
}