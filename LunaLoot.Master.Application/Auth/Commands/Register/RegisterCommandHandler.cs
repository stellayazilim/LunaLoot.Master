using ErrorOr;
using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.Infrastructure.Common.Errors;
using LunaLoot.Master.Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace LunaLoot.Master.Application.Auth.Commands.Register;


internal sealed class RegisterCommandHandler
(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> hasher)
    : IRequestHandler<RegisterCommand, ErrorOr<RegisterCommandResult>>
{
    
    public async Task<ErrorOr<RegisterCommandResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        var user = new ApplicationUser()
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobilePhoneNumber = request.MobilePhoneNumber,
            RefreshTokens = new string[]{},
            Email = request.Email,
            
        };
        user.PasswordHash = hasher.HashPassword(user, request.Password);

        var result = await userManager.CreateAsync(user);

       
        if (!result.Succeeded)
        {
            return result.Errors.ConvertToErrorOr();
        }
        return new RegisterCommandResult();
    }
}