using ErrorOr;
using LunaLoot.Master.Infrastructure.Common.Errors;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace LunaLoot.Master.Application.Auth.Commands.Register;


internal sealed class RegisterCommandHandler
(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> hasher)
    : IRequestHandler<RegisterCommand, ErrorOr<RegisterCommandResult>> {
    
    public async Task<ErrorOr<RegisterCommandResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        ApplicationUser user = new();

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UserName = request.Email;
        user.MobilePhoneNumber = request.MobilePhoneNumber;
        user.PasswordHash = request.Password;
        
        var result = await userManager.CreateAsync(user, user.PasswordHash);

       
        if (!result.Succeeded)
        {
            return result.Errors.ConvertToErrorOr();
        }
        return new RegisterCommandResult();
    }
}