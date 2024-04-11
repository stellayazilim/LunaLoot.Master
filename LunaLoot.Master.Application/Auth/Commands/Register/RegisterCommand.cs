using ErrorOr;
using MediatR;

namespace LunaLoot.Master.Application.Auth.Commands.Register;

public record RegisterCommand(
    string FirstName, 
    string LastName, 
    string MobilPhoneNumber, 
    string Email, 
    string Password)
    : IRequest<ErrorOr<RegisterCommandResult>>
{
    public string FirstName = FirstName;
    public string LastName = LastName;
    public string MobilPhoneNumber = MobilPhoneNumber;
    public string Email = Email;
    public string Password = Password;
}


