using Duende.IdentityServer.EntityFramework.Entities;
using LunaLoot.Master.Infrastructure.Context;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Abstractions;
using Moq;

namespace __mocks__;

public class MockUserManager : IDisposable
{

    private UserStore<ApplicationUser, IdentityRole<Guid>, LunaLootMasterDbContext, Guid> _userStore;
    private LunaLootMasterDbContext _dbContext;
    private UserManager<ApplicationUser> _userManager;
    public MockUserManager(MockLunaLootMasterDbContext m )
    {
        _dbContext = m.GetContext();
        _userStore = new UserStore<ApplicationUser, IdentityRole<Guid>,LunaLootMasterDbContext, Guid>(_dbContext, null);
        IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        _userManager = new UserManager<ApplicationUser>(_userStore, null, passwordHasher, null, null, null, null, null, new Logger<UserManager<ApplicationUser>>(NullLoggerFactory.Instance));
    }

   

     public UserManager<ApplicationUser> GetUserManager()
    {
        return _userManager;
    }


    public  async Task AddMockUsers(List<ApplicationUser> users)
    {
        foreach (var x in users)
        {
            await _userManager.CreateAsync(x);
        }
    }

 

 
    public void Dispose()
    { 
      _dbContext.Dispose();
      _userManager.Dispose();
     
    }

  
}