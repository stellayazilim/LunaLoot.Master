using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.Auth.Common.Providers;

public class ApplicationPasswordValidator
    (IdentityErrorDescriber? errors = null) : PasswordValidator<ApplicationUser>
{
    /// <summary>
    /// Gets the <see cref="IdentityErrorDescriber"/> used to supply error text.
    /// </summary>
    /// <value>The <see cref="IdentityErrorDescriber"/> used to supply error text.</value>
    private new IdentityErrorDescriber Describer { get; } = errors ?? new IdentityErrorDescriber();

    /// <summary>
    /// Validates a password as an asynchronous operation.
    /// </summary>
    /// <param name="manager">The <see cref="UserManager{TUser}"/> to retrieve the <paramref name="user"/> properties from.</param>
    /// <param name="user">The user whose password should be validated.</param>
    /// <param name="password">The password supplied for validation</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public override Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
    {
        List<IdentityError>? errors = null;
        var options = manager.Options.Password;
        if (string.IsNullOrWhiteSpace(password) || password.Length < options.RequiredLength)
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.PasswordTooShort(options.RequiredLength));
        }
        if (options.RequireNonAlphanumeric && (password ?? string.Empty).All(IsLetterOrDigit))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.PasswordRequiresNonAlphanumeric());
        }
        if (options.RequireDigit && (password ?? string.Empty).Any(IsDigit))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.PasswordRequiresDigit());
        }
        if (options.RequireLowercase && (password ?? string.Empty).Any(IsLower))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.PasswordRequiresLower());
        }
        if (options.RequireUppercase && (password ?? string.Empty).Any(IsUpper))
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.PasswordRequiresUpper());
        }
        if (options.RequiredUniqueChars >= 1 && (password ?? string.Empty).Distinct().Count() < options.RequiredUniqueChars)
        {
            errors ??= new List<IdentityError>();
            errors.Add(Describer.PasswordRequiresUniqueChars(options.RequiredUniqueChars));
        }
        return
            Task.FromResult(errors?.Count > 0
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success);
    }

    /// <summary>
    /// Returns a flag indicating whether the supplied character is a digit.
    /// </summary>
    /// <param name="c">The character to check if it is a digit.</param>
    /// <returns>True if the character is a digit, otherwise false.</returns>
    public override bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    /// <summary>
    /// Returns a flag indicating whether the supplied character is a lower case ASCII letter.
    /// </summary>
    /// <param name="c">The character to check if it is a lower case ASCII letter.</param>
    /// <returns>True if the character is a lower case ASCII letter, otherwise false.</returns>
    public override bool IsLower(char c)
    {
        return c >= 'a' && c <= 'z';
    }

    /// <summary>
    /// Returns a flag indicating whether the supplied character is an upper case ASCII letter.
    /// </summary>
    /// <param name="c">The character to check if it is an upper case ASCII letter.</param>
    /// <returns>True if the character is an upper case ASCII letter, otherwise false.</returns>
    public override bool IsUpper(char c)
    {
        return c >= 'A' && c <= 'Z';
    }

    /// <summary>
    /// Returns a flag indicating whether the supplied character is an ASCII letter or digit.
    /// </summary>
    /// <param name="c">The character to check if it is an ASCII letter or digit.</param>
    /// <returns>True if the character is an ASCII letter or digit, otherwise false.</returns>
    public override bool IsLetterOrDigit(char c)
    {
        return IsUpper(c) || IsLower(c) || IsDigit(c);
    }
}