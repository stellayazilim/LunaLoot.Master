
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace __mocks__;

public class MockLunaLootMasterDbContext : IDisposable
{

    private  LunaLootMasterDbContext _dbContext;
     public MockLunaLootMasterDbContext()
    {
        var options = new DbContextOptionsBuilder<LunaLootMasterDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _dbContext = new LunaLootMasterDbContext(options);
    }




    public  LunaLootMasterDbContext GetContext()
    {
        return _dbContext;
    }

 


    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }


}