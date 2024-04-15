
using FluentAssertions;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using LunaLoot.Master.Infrastructure.UnitTests.__mocks__;
using LunaLoot.Master.Infrastructure.UnitTests.Utils.Constants;
using Microsoft.AspNetCore.Identity;
using Moq;
using NSubstitute;

namespace LunaLoot.Master.Infrastructure.UnitTests.Auth.Tests;

public class ApplicationUserValidator_Tests
{

    ApplicationUserValidator _validator = new ApplicationUserValidator(Constants.ErrorDescriber);

    [Theory]
    [MemberData(nameof(ConstructorData))]
    public void TestApplicationUserValidator_WhenConstructed_ShouldConstructed(IdentityErrorDescriber describer)
    {
        // act
        var validator = new ApplicationUserValidator(describer);
        
        // assert
        
        validator.Describer.Should().NotBeNull();
        
    }

    [Fact]
    public async void TestApplicationUserValidator_WithValidUser_ShouldValidate()
    {
        // arrange 
       
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Email = Constants.Email
        };

        var userManager = __Mocks__.MockUserManager();
        userManager.Object.Options.User.RequireUniqueEmail = true;

        userManager.Setup(x => x.GetEmailAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(Constants.Email);
        userManager.Setup(x => x.FindByEmailAsync(Constants.Email)).ReturnsAsync(user);
        userManager.Setup(x => x.GetUserIdAsync(user)).ReturnsAsync(user.Id.ToString());
        userManager.Setup(x => x.GetUserIdAsync(user)).ReturnsAsync(Guid.NewGuid().ToString);
       
        // act
        var result = await _validator.ValidateAsync(userManager.Object, user);
        
        // assert

        result.Succeeded.Should().BeTrue();
        result.Errors.Count().Should().Be(0);
    }


    [Fact]
    public async void TestApplicationUserValidator_WithInvalidEmail_ShouldReturnInvalidEmailError()
    {
        // arrange
        var mockUserManager = __Mocks__.MockUserManager();
        var user = new ApplicationUser()
        {
            Email = Constants.InvalidEmail
        };
        mockUserManager.Object.Options.User.RequireUniqueEmail = true;
        mockUserManager.Setup(x => x.GetEmailAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(user.Email);
        
        // act
        var result = await _validator.ValidateAsync(mockUserManager.Object, user);
        // assert
        
        mockUserManager.Verify(x => x.GetEmailAsync(user), Times.Exactly(1));
        mockUserManager.Verify(x => x.FindByEmailAsync(user.Email), Times.Never);
    }

    [Fact]
    public async void TestApplicationUesrValidator_WithNullEmail_ShouldReturnInvalidEmailError()
    {
        // arrange 
        var mockUserManger = __Mocks__.MockUserManager();
        mockUserManger.Object.Options.User.RequireUniqueEmail = true;
        var user = new ApplicationUser
        {
            Email = " "
        };
        // act
        var result = await _validator.ValidateAsync(mockUserManger.Object, user);

        // assert

        result.Errors.Count().Should().BePositive();
        result.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async void TestApplicationUserValidator_WhenDuplicateEmail_ShouldeturnDuplicateEmailError()
    {
        // arrange
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = Constants.Email
        };
        var mockUserManger = __Mocks__.MockUserManager();
        mockUserManger.Object.Options.User.RequireUniqueEmail = true;  

        mockUserManger.Setup(x => x.GetEmailAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(Constants.Email);
        mockUserManger.Setup(x => x.FindByEmailAsync(Constants.Email)).ReturnsAsync(user);
        
            // return owner id
        mockUserManger.SetupSequence(x => x.GetUserIdAsync(user))
            .ReturnsAsync(Guid.NewGuid().ToString())
            .ReturnsAsync(user.Id.ToString());
        
        
        // act
        var result = await _validator.ValidateAsync(mockUserManger.Object, user);

        // assert
        
        mockUserManger.Verify(x => x.GetUserIdAsync(It.IsAny<ApplicationUser>()), Times.Exactly(2));
        result.Succeeded.Should().BeFalse();
        result.Errors.FirstOrDefault()!.Code.Should().Be(Constants.ErrorDescriber.DuplicateEmail(Constants.Email).Code);
        result.Errors.FirstOrDefault()!.Description.Should()
            .Be(Constants.ErrorDescriber.DuplicateEmail(Constants.Email).Description);

    }
    public static IEnumerable<object[]> ConstructorData()
    {
        var describers = new List<IdentityErrorDescriber>
        {
            Constants.ErrorDescriber,
            null
        };

        foreach (var (describer, index) in describers.Select((x, y)=> (x,y)))
        {
            yield return [ describer ];
        }
    }
  
    
}