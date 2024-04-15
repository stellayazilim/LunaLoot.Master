using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LunaLoot.Master.Infrastructure.UnitTests.__mocks__;

// ReSharper disable once InconsistentNaming
public static class __Mocks__
{

        public static Mock<ApplicationUserManager> MockUserManager(List<ApplicationUser>? ls = null) 
        {

            var res = IdentityResult.Success;
        
            var store = new Mock<IUserStore<ApplicationUser>>();
            store.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>())) 
                .ReturnsAsync(IdentityResult.Success);
            
            var mgr = new Mock<ApplicationUserManager>(
                Mock.Of<IUserStore<ApplicationUser>>(),  null, null, null, null, null, null, null, null);
            
        
            
            if (ls is not null)
                mgr.Setup( x => x.Users).Returns(ls.AsQueryable);
         

            return mgr;
   
    }
}
