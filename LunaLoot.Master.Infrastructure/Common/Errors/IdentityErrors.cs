using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.Common.Errors;

public static class IdentityErrors
{
    public static List<Error> ConvertToErrorOr(this IEnumerable<IdentityError> errors)
    {
        List<Error> errorList = new();

        foreach (var error in errors)
        {
            errorList.Add(Error.Failure(
                code:error.Code,
                description:error.Description));
        }

        return errorList;
    }
}