using FakeItEasy;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LunaLoot.Master.Infrastructure.UnitTests.Persistence.Configurations.Utils;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Ploeh.AutoFixture;

namespace LunaLoot.Master.Infrastructure.UnitTests.Persistence.Configurations.Tests;

public class ApplicationUserConfigurationTest
{
    

    
    /// @todo test with mock object
    [Fact]
    public void TestApplicationUserMapping_WhenConfiguration_ShouldConfigure()
    {
        // arrange
        var  typeBuilder =
            Utils.Utils.CreateEntityTypeBuilder<ApplicationUser>();
        var configuration = new ApplicationUserMapping();
        // act
        configuration.Configure(typeBuilder);
        // assert
      
    }

}