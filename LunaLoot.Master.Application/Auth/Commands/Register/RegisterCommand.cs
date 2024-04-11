using System.ComponentModel.DataAnnotations;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace LunaLoot.Master.Application.Auth.Commands.Register;

public record RegisterCommand(

    string FirstName,

    string LastName,

    string MobilePhoneNumber,

    string Email,

    string Password)
    : IRequest<ErrorOr<RegisterCommandResult>>;


public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.LastName).NotNull().NotNull();
        RuleFor(x => x.MobilePhoneNumber).NotNull().NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
    }
}


