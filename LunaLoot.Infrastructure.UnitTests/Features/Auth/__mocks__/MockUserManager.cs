
using Microsoft.AspNetCore.Identity;
using Moq;

namespace LunaLoot.Infrastructure.UnitTests.Features.Auth.__mocks__;


// ReSharper disable once InconsistentNaming
public static class __Mocks__
{

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser>? ls = null) where TUser : class
        {

            var res = IdentityResult.Success;
        
            var store = new Mock<IUserStore<TUser>>();
            store.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<CancellationToken>())) 
                .ReturnsAsync(IdentityResult.Success);
            
            var mgr = new Mock<UserManager<TUser>>(
                Mock.Of<IUserStore<TUser>>(),  null, null, null, null, null, null, null, null);
            
        
            
            if (ls is not null)
                mgr.Setup( x => x.Users).Returns(ls.AsQueryable);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(),It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            return mgr;
   
    }
}
