using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Identity.Client;

namespace __mocks__;

public class MockRoleManager: IDisposable
{
    public void Dispose()
    {
        _dbContext.Dispose();
        _roleStore.Dispose();
        _roleManager.Dispose();
    }
    
    private readonly LunaLootMasterDbContext _dbContext;
    private IRoleStore<IdentityRole<Guid>> _roleStore;
    private RoleManager<IdentityRole<Guid>> _roleManager;

    public MockRoleManager(MockLunaLootMasterDbContext m)
    {
        _dbContext = m.GetContext();
        _roleStore = new RoleStore<IdentityRole<Guid>, LunaLootMasterDbContext, Guid>(_dbContext, null);
        var logger = new Logger<RoleManager<IdentityRole<Guid>>>(new LoggerFactory());
        _roleManager = new RoleManager<IdentityRole<Guid>>(
            store: _roleStore,
            logger:logger,
            roleValidators:null,
            keyNormalizer:null,
            errors:null);
    }


    public RoleManager<IdentityRole<Guid>> GetManager()
    {
        return _roleManager;
    }

}