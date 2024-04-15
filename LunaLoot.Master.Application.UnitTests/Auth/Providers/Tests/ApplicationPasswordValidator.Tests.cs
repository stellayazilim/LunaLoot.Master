using FluentAssertions;
using LunaLoot.Master.Application.UnitTests.Features.Auth.Utils;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using LunaLoot.Master.Infrastructure.UnitTests.Auth.Providers.ProviderUtils;
using Microsoft.AspNetCore.Identity;
using Moq;
using Mocks = LunaLoot.Infrastructure.UnitTests.Auth.__mocks__.Mocks;

namespace LunaLoot.Master.Application.UnitTests.Auth.Providers.Tests;

public class ApplicationPasswordValidatorTests
{
    
    private class  CustomDescriber:
        IdentityErrorDescriber;
    private readonly Mock<ApplicationUserManager> _userManager = Mocks.MockUserManager(It.IsAny<List<ApplicationUser>?>());


    [Fact]
    public void ApplicationPasswordValidator_WhenConstructedWithoutDescriber_ShouldInitializeWithDefaultDescriber()
    {
        // arrange
        var passwordValidator = new ApplicationPasswordValidator();
        // act
        var describer = passwordValidator.Describer;
        // assert
        describer.Should().BeOfType<IdentityErrorDescriber>();
        describer.Should().NotBeNull();
    }

    
    [Fact]
    public void ApplicationPasswordValidator_WhenConstructedWithDescriber_ShouldInitializeWithGivenDescriber()
    {   
        // arrange
        var customDescriber = new CustomDescriber();
        
        // act
        var passwordValidator = new ApplicationPasswordValidator(customDescriber);
        
        // assert
        passwordValidator.Describer.Should().BeOfType<CustomDescriber>();
    }

    [Theory]
    [MemberData(nameof(CreateInvalidPasswordsInFacts))]
    public async void ApplicationPasswordValidator_WhenValidatingInvalidPassword_ShouldReturnError(
        string? password,
        IdentityResult? expectedResult
        )
    {
        // arrange 
        #region arrange

            var passwordValidator = new ApplicationPasswordValidator();
            var user = AuthUtils.CreateUser(null);

        #endregion
       
       
        // act
        #region act
            var result = await passwordValidator.ValidateAsync(
                _userManager.Object,
                user,
                password);
        #endregion

        // assert
        #region assert

            result.Succeeded.Should().BeFalse();

            result.Errors.Select((v, i) => (v, i)).ToList().ForEach((v) =>
            {
                v.v.Code.Should().Be(expectedResult?.Errors.ToList()[v.i].Code);
                v.v.Description.Should().Be(expectedResult?.Errors.ToList()[v.i].Description);
            });

        #endregion
        
    }

    public static IEnumerable<object[]> CreateInvalidPasswordsInFacts()
    {
        foreach (ApplicationPasswordValidatorUtils.InvalidPassword objects in ApplicationPasswordValidatorUtils.CreateInvalidPasswords)
        {
            var password = objects.Password;
            var expectedResult = objects.Errors;
            yield return [ password, expectedResult];
        }
    }
}