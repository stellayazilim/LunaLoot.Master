using LunaLoot.Master.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace __mocks__;

public class MockLunaLootMasterDbContext
{

    private readonly LunaLootMasterDbContext _dbContext;
     public MockLunaLootMasterDbContext()
    {
        var options = new DbContextOptionsBuilder<LunaLootMasterDbContext>()
            .UseInMemoryDatabase(databaseName: "LunaLoot.Master")
            .Options;
        _dbContext = new LunaLootMasterDbContext(options);
    }
    public  LunaLootMasterDbContext GetContext()
    {
        return _dbContext;
    }
}