using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Mappings;


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