using FakeItEasy;
using Microsoft.EntityFrameworkCore;


namespace LunaLoot.Master.Infrastructure.UnitTests.Persistence.Context.LunaLootMasterDbContext.Tests;


/// <summary>
/// This class inherits LunaLootMasterDbContexts
/// to test protected OnModelCreating method
/// </summary>
public class LunaLootMasterDbContextTests()
    : Infrastructure
        .Persistence
        .EFCore
        .Context
        .LunaLootMasterDbContext(OptionsBuilder())
{
    [Fact]
    public void Test1()
    { 
        
    // arrange
        var mockModelBuilder = A.Fake<ModelBuilder>();

    // act
        base.OnModelCreating(mockModelBuilder);
    
    
    //assert
        A.CallTo(() => 
               mockModelBuilder
                   .ApplyConfigurationsFromAssembly(
                       typeof(
                           Infrastructure
                           .Persistence
                           .EFCore
                           .Context
                           .LunaLootMasterDbContext).Assembly, null ))
       .MustHaveHappenedOnceExactly();
    }
    
    
    /// <summary>
    /// Create Fake Db options
    /// </summary>
    /// <returns> DbContextOptions </returns>
    private static DbContextOptions OptionsBuilder ()  {
        var optionsBuilder = new DbContextOptionsBuilder<Infrastructure.Persistence.EFCore.Context.LunaLootMasterDbContext>();
        optionsBuilder.UseInMemoryDatabase("test_db");
        return optionsBuilder.Options;
    }
}