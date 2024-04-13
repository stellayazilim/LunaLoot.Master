using ErrorOr;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Common.Errors;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LunaLoot.Master.Application.Auth.Commands.Register;


internal sealed class RegisterCommandHandler
(ApplicationUserManager userManager, ILogger<RegisterCommandHandler> logger)
    : IRequestHandler<RegisterCommand, ErrorOr<RegisterCommandResult>> {
    
    public async Task<ErrorOr<RegisterCommandResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {

        logger.LogDebug("handler");
        ApplicationUser user = new();

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UserName = request.Email;
        user.MobilePhoneNumber = request.MobilePhoneNumber;
        user.PasswordHash = request.Password;
        
        var result = await userManager.CreateAsync(user, user.PasswordHash);

        logger.LogDebug(result.Succeeded.ToString());
       
        if (!result.Succeeded)
        {
            return result.Errors.ConvertToErrorOr();
        }
        return new RegisterCommandResult();
    }
}