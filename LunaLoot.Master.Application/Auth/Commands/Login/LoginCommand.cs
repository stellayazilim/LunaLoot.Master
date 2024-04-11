using System.ComponentModel.DataAnnotations;
using ErrorOr;
using MediatR;

namespace LunaLoot.Master.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<ErrorOr<LoginCommandResult>>
{
    
    
    
    [Required, EmailAddress]
    public required string Email;
    
    [Required, StringLength(255, MinimumLength = 8, ErrorMessage = "Password can not be shorter than 8 char")]
    public required string Password;
}
  

    
    
    