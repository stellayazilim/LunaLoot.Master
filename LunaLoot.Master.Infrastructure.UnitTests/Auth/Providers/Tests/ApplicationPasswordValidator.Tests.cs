using FluentAssertions;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.UnitTests.Auth.__mocks__;
using LunaLoot.Master.Infrastructure.UnitTests.Auth.Providers.ProviderUtils;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit.Abstractions;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Providers.Tests;

public class ApplicationPasswordValidatorTests
{
    
    private class  CustomDescriber:
        IdentityErrorDescriber;
    private readonly Mock<ApplicationUserManager> _userManager;
    public ApplicationPasswordValidatorTests(ITestOutputHelper testOutputHelper)
    {
        _userManager = __Mocks__.MockUserManager(null);
    }



    [Fact]
    public void ApplicationPasswordValidator_WhenContructedWithoutDescriber_ShouldInitalizeWithDefaultDescriber()
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
    public void ApplicationPasswordValidator_WhenConstructedWithDescriber_ShoulInitalizeWithGivenDescriber()
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
        var passwordValidator = new ApplicationPasswordValidator();
        var user = AuthUtils.AuthUtils.CreateUser(null);
       
        // act
        var result = await passwordValidator.ValidateAsync(
            _userManager.Object,
            user,
            password);
        
        // assert
        result.Succeeded.Should().BeFalse();

        result.Errors.Select((v, i) => (v, i)).ToList().ForEach((v) =>
        {
            v.v.Code.Should().Be(expectedResult?.Errors.ToList()[v.i].Code);
            v.v.Description.Should().Be(expectedResult?.Errors.ToList()[v.i].Description);
        });
        
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