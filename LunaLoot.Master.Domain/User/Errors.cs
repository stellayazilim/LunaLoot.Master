using ErrorOr;

namespace LunaLoot.Master.Domain.User;

public static partial class Errors
{
    public static Error DuplicateEmail (string email) => 
        Error.Conflict(
            code: $"Email \"{email}\" already exists",
            description: "Email conflict"
        );

    public static Error DuplicateMobilPhoneNumber(string mobilPhoneNumber) => 
        Error.Conflict(
            code: $"Phone \"{mobilPhoneNumber}\" already exits",
            description: "Phone conflict"
        );
}