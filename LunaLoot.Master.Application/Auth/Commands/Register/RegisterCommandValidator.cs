using FluentValidation;

namespace LunaLoot.Master.Application.Auth.Commands.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
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


