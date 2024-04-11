using ErrorOr;
using LunaLoot.Master.Domain.User;
using LunaLoot.Master.Domain.User.ValueObjects;
using LunaLoot.Master.Infrastructure.Auth;
using LunaLoot.Master.Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LunaLoot.Master.Application.Auth.Commands.Register;


public class RegisterCommandHandler
(UserManager<ApplicationUser> userManager)
    : IRequestHandler<RegisterCommand, ErrorOr<RegisterCommandResult>>
{
    
    public async Task<ErrorOr<RegisterCommandResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var hasher = new PasswordHasher();
        var user = new ApplicationUser()
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobilePhoneNumber = request.MobilPhoneNumber,
            RefreshTokens = new string[]{},
            Email = request.Email,
            
        };
        user.PasswordHash = hasher.HashPassword(user, request.Password);

        var result = await userManager.CreateAsync(user);


    
        return result.Succeeded ? new RegisterCommandResult() : new Error();
    }
}