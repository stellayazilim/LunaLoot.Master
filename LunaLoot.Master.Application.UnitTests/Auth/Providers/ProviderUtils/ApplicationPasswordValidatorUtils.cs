using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Providers.ProviderUtils;



public static class ApplicationPasswordValidatorUtils
{


    public static IdentityErrorDescriber Describer = new IdentityErrorDescriber();
    public static PasswordOptions PasswordOptions = new PasswordOptions();
    public record InvalidPassword(
        string? Password,
        IdentityResult? Errors
    );
    public static IEnumerable<InvalidPassword> CreateInvalidPasswords =>
      new []
      {
          new InvalidPassword(
                Password: " ",
                Errors: IdentityResult.Failed([
                    Describer.PasswordTooShort(PasswordOptions.RequiredLength),
                    Describer.PasswordRequiresNonAlphanumeric(),
                    Describer.PasswordRequiresDigit(),
                    Describer.PasswordRequiresLower(),
                    Describer.PasswordRequiresUpper(),
                    Describer.PasswordRequiresUniqueChars(PasswordOptions.RequiredUniqueChars)
                ])
           ),
          new InvalidPassword(
                Password: null,
                Errors: IdentityResult.Failed([
                    Describer.PasswordTooShort(PasswordOptions.RequiredLength),
                    Describer.PasswordRequiresNonAlphanumeric(),
                    Describer.PasswordRequiresDigit(),
                    Describer.PasswordRequiresLower(),
                    Describer.PasswordRequiresUpper(),
                    Describer.PasswordRequiresUniqueChars(PasswordOptions.RequiredUniqueChars)
                ])),
          new InvalidPassword(
              Password:"12345asdfgh123",
              Errors: IdentityResult.Failed(
                    [
                       Describer.PasswordRequiresNonAlphanumeric(),
                       Describer.PasswordRequiresUpper(),
                       Describer.PasswordRequiresUniqueChars(1)
                    ]
                  )),
          new InvalidPassword(
              Password: "Hello124",
              Errors: IdentityResult.Failed([
                  Describer.PasswordRequiresNonAlphanumeric(),
              ])),
          new InvalidPassword(
              Password: "He124",
              Errors: IdentityResult.Failed([
                  Describer.PasswordTooShort(PasswordOptions.RequiredLength),
                  Describer.PasswordRequiresNonAlphanumeric(),
              ])),
          new InvalidPassword(
              Password: "He12!",
              Errors: IdentityResult.Failed([
                  Describer.PasswordTooShort(PasswordOptions.RequiredLength),
              ]))
      };
}

