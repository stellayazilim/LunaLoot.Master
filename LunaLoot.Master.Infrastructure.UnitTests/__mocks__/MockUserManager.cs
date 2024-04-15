using FakeItEasy;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;
namespace LunaLoot.Master.Infrastructure.UnitTests.__mocks__;

// ReSharper disable once InconsistentNaming
public static class __Mocks__
{

        public static ApplicationUserManager MockUserManager(List<ApplicationUser>? ls = null) 
        {

            var res = IdentityResult.Success;
            var _store = A.Fake<IUserStore<ApplicationUser>>();
            A.CallTo(() => _store.CreateAsync(A<ApplicationUser>._, A<CancellationToken>._)).Returns(IdentityResult.Success);


            var userManager = A.Fake<ApplicationUserManager>();
            A.CallTo(() => userManager.CreateAsync(A<ApplicationUser>._, A<string>._)).Returns(res);

            if (ls is not null)
                A.CallTo(() => userManager.Users).Returns(ls.AsQueryable());
    

            return userManager;
   
    }
}
