using LunaLoot.Master.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace __mocks__;

public class MockLunaLootMasterDbContext
{
    public static LunaLootMasterDbContext GetContext()
    {
        var options = new DbContextOptionsBuilder<LunaLootMasterDbContext>()
            .UseInMemoryDatabase(databaseName: "LunaLoot.Master")
            .Options;
        var dbContext = new LunaLootMasterDbContext(options);

        return dbContext;
    }
}