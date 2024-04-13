using System.ComponentModel.DataAnnotations;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.Auth.Common.Providers;
public class ApplicationUserValidator: UserValidator<ApplicationUser>
{
    
    public ApplicationUserValidator(IdentityErrorDescriber? errors = null)
    {
        Describer = errors ?? new IdentityErrorDescriber();
    }

  
    public IdentityErrorDescriber Describer { get; private set; }


    public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
     
        var errors = await ValidateUserName(manager, user).ConfigureAwait(false);
        if (manager.Options.User.RequireUniqueEmail)
        {
            errors = await ValidateEmail(manager, user, errors).ConfigureAwait(false);
        }
        return errors?.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }
    
    
    
    private async Task<List<IdentityError>?> ValidateUserName(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        List<IdentityError>? errors = null;
        var userName = await manager.GetUserNameAsync(user).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(userName))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.InvalidUserName(userName));
        }
        else if (!string.IsNullOrEmpty(manager.Options.User.AllowedUserNameCharacters) &&
            userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.InvalidUserName(userName));
        }
        else
        {
            var owner = await manager.FindByNameAsync(userName).ConfigureAwait(false);
            if (owner != null &&
                !string.Equals(await manager.GetUserIdAsync(owner).ConfigureAwait(false), await manager.GetUserIdAsync(user).ConfigureAwait(false)))
            {
                errors ??= new List<IdentityError>();
                errors.Add(Describer.DuplicateUserName(userName));
            }
        }

        return errors;
    }

    // make sure email is not empty, valid, and unique
    private async Task<List<IdentityError>?> ValidateEmail(UserManager<ApplicationUser> manager, ApplicationUser user, List<IdentityError>? errors)
    {
        var email = await manager.GetEmailAsync(user).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(email))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.InvalidEmail(email));
            return errors;
        }
        if (!new EmailAddressAttribute().IsValid(email))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.InvalidEmail(email));
            return errors;
        }
        var owner = await manager.FindByEmailAsync(email).ConfigureAwait(false);
        if (owner != null &&
            !string.Equals(await manager.GetUserIdAsync(owner).ConfigureAwait(false), await manager.GetUserIdAsync(user).ConfigureAwait(false)))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.DuplicateEmail(email));
        }
        return errors;
    }
}