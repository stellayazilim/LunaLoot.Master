using __mocks__;
using __stubs__;
using FluentAssertions;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace TestLunaLootMasterInfrastructure;

public class TestUserManager
{

    [Fact]
    public void ShouldHashPassword()
    {
        IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser();
        user.PasswordHash = passwordHasher.HashPassword(user, "Test!12");
        user.PasswordHash.Should().NotBeEmpty();
        user.PasswordHash.Should().NotBeNull();
        user.PasswordHash.Should().NotBe("Test!12");
    }
    [Fact]
    public async void ShouldRegisterUser()
    {

        var m = new MockUserManager();
        var passwordHasher = m.GetUserManager().PasswordHasher;
        
        
        var user = new ApplicationUser
        {
            Email = "jhondoe@example.com",

        };
        user.PasswordHash =  passwordHasher.HashPassword(user, "Test!12");
        var res =  await m.GetUserManager().CreateAsync(user);

        res.Succeeded.Should().Be(true);
        res.Errors.Should().HaveCount(0);
        res.Errors.Should().BeEmpty();

    }


    [Fact]
    public async void ShouldListUsers()
    {
        var users = UsersStubs.UserStubs();
        var m = new MockUserManager();
         m.AddMockUsers(users);
        var dbUses =  m.GetUserManager().Users;
        dbUses.Should().NotBeNull();
        m.GetUserManager().Users.Count().Should().Be(2);
      
    }

    [Fact]
    public async void ShouldGetUserByEmail()
    {
        var m = new MockUserManager();
        ApplicationUser result = ( await m.GetUserManager().FindByEmailAsync("jhondoe2@example.com"))!;

        result.Email.Should().Be("jhondoe2@example.com");
    }
}