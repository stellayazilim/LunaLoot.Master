using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LunaLoot.Master.Infrastructure.Auth.Common.Services;

public class ApplicationUserValidator: UserValidator<ApplicationUser>
{
    /// <summary>
    /// Validates user 
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <returns> IdentityResult </returns>
    /// <exception cref="NotImplementedException"></exception>
    public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        var exist =  manager.FindByEmailAsync(user.Email!);


        if (user.Email is null)
        {
            return IdentityResult.Failed(
                new IdentityError[]
                {
                    new IdentityErrorDescriber().InvalidEmail(user.Email!)   
                }
            );
        }
        
        if (exist.Result is not null)
            return IdentityResult.Failed(
                    new IdentityError[]
                    {
                       new IdentityErrorDescriber().DuplicateEmail(user.Email!)   
                    }
                );
        
        
        throw new NotImplementedException();
    }
}