
using FakeItEasy;
using FluentAssertions;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using LunaLoot.Master.Infrastructure.UnitTests.__mocks__;
using LunaLoot.Master.Infrastructure.UnitTests.Utils.Constants;
using Microsoft.AspNetCore.Identity;
using Moq;
using Times = Moq.Times;

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
        userManager.Options.User.RequireUniqueEmail = true;

        A.CallTo(() => userManager.GetEmailAsync(A<ApplicationUser>._)).Returns(Constants.Email);
        A.CallTo(() => userManager.FindByEmailAsync(A<string>._)).Returns(user);
        A.CallTo(() => userManager.GetUserIdAsync(A<ApplicationUser>._)).Returns(user.Id.ToString());
        A.CallTo(() => userManager.GetUserIdAsync(A<ApplicationUser>._)).Returns(Guid.NewGuid().ToString());
        

        // act
        var result = await _validator.ValidateAsync(userManager, user);
        
        // assert

        result.Succeeded.Should().BeTrue();
        result.Errors.Count().Should().Be(0);
    }


    [Fact]
    public async void TestApplicationUserValidator_WithInvalidEmail_ShouldReturnInvalidEmailError()
    {
        // arrange
        var userManager = __Mocks__.MockUserManager();
        userManager.Options.User.RequireUniqueEmail = true;
        var user = new ApplicationUser()
        {
            Email = Constants.InvalidEmail
        };
        A.CallTo(() => userManager.GetEmailAsync(A<ApplicationUser>._)).Returns(user.Email);
        
        // act
        var result = await _validator.ValidateAsync(userManager, user);
        // assert

        A.CallTo(() => userManager.GetEmailAsync(user)).MustHaveHappenedOnceExactly();
        A.CallTo(() => userManager.FindByIdAsync(user.Email)).MustNotHaveHappened();
    }

    [Fact]
    public async void TestApplicationUesrValidator_WithNullEmail_ShouldReturnInvalidEmailError()
    {
        // arrange 
        var userManager = __Mocks__.MockUserManager();
        userManager.Options.User.RequireUniqueEmail = true;
        var user = new ApplicationUser()
        {
            Email = string.Empty
        };
        // act
        var result = await _validator.ValidateAsync(userManager, user);

        // assert

        result.Errors.Count().Should().BePositive();
        result.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async void TestApplicationUserValidator_WhenDuplicateEmail_ShouldeturnDuplicateEmailError()
    {
        // arrange
        // arrange 
       
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            Email = Constants.Email
        };

        var userManager = __Mocks__.MockUserManager();
        userManager.Options.User.RequireUniqueEmail = true;

        A.CallTo(() => userManager.GetEmailAsync(A<ApplicationUser>._)).Returns(user.Email);
        A.CallTo(() => userManager.FindByEmailAsync(user.Email)).Returns(user);

        
            // return owner id
        A.CallTo(() => userManager.GetUserIdAsync(A<ApplicationUser>._)).Returns(user.Id.ToString()).Once();
        A.CallTo(() => userManager.GetUserIdAsync(A<ApplicationUser>._)).Returns(Guid.NewGuid().ToString()).Once();

        
        // act
        var result = await _validator.ValidateAsync(userManager, user);

        // assert
        A.CallTo(() => userManager.GetUserIdAsync(user)).MustHaveHappenedTwiceExactly();
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