using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.Auth.Common.Services;

public class ApplicationPasswordValidator: PasswordValidator<ApplicationUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUserValidator> manager, ApplicationUserValidator user, string? password)
    {


        return Task.FromResult(new IdentityResult());
    }
}