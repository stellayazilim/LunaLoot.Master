using System.ComponentModel.DataAnnotations;
using System.Data;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace LunaLoot.Master.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<ErrorOr<LoginCommandResult>>
{
    [Required, EmailAddress]
    public required string Email;
    
    [Required, StringLength(255, MinimumLength = 8, ErrorMessage = "Password can not be shorter than 8 char")]
    public required string Password;
}



public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();

        RuleFor(x => x.Password).MinimumLength(8).MaximumLength(255);
       
    }
}
    