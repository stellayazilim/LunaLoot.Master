using ErrorOr;
using LunaLoot.Master.Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Application.Auth.Commands.Login;
public class LoginCommandHandler(
        UserManager<ApplicationUser> userManager
    )
    : IRequestHandler<LoginCommand, ErrorOr<LoginCommandResult>>
{


    
    
    public async  Task<ErrorOr<LoginCommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = await userManager.FindByEmailAsync(request.Email);
        
        
        return new LoginCommandResult(
            AccessToken : user is null ? "no token":"token"
            );
    }
}