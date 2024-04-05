using Duende.IdentityServer.EntityFramework.Entities;
using LunaLoot.Master.Infrastructure.Context;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace __mocks__;

public class MockUserManager
{

    private readonly UserManager<ApplicationUser> _userManager;

    public MockUserManager()
    {
        var m = new MockLunaLootMasterDbContext();  
        LunaLootMasterDbContext dbContext = m.GetContext();
      
        

        IUserStore<ApplicationUser> mockUserStore = new UserStore<ApplicationUser, IdentityRole<Guid>,LunaLootMasterDbContext, Guid>(dbContext, null);
        IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(mockUserStore, null, passwordHasher, null, null, null, null, null, null);
        
        _userManager = userManager;
    }
    public UserManager<ApplicationUser> GetUserManager()
    {
        return _userManager;
    }


    public  async void AddMockUsers(List<ApplicationUser> users)
    {
        foreach (var x in users)
        {
            await _userManager.CreateAsync(x);
        }
    }
}