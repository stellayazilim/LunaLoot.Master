using __mocks__;
using __stubs__;
using FluentAssertions;
using LunaLoot.Master.Domain.User;
using LunaLoot.Master.Domain.User.ValueObjects;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace TestLunaLootMasterInfrastructure;


public class TestUserManager
{

    [Fact]
    public void ShouldHashPassword()
    {
        IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser()
        {
            FirstName = "jhown",
            LastName = "doe",
            RefreshTokens = new string[]{}
        };
        user.PasswordHash = passwordHasher.HashPassword(user, "Test!12");
        user.PasswordHash.Should().NotBeEmpty();
        user.PasswordHash.Should().NotBeNull();
        user.PasswordHash.Should().NotBe("Test!12");
    }
    
    
    [Fact]
    public async void ShouldRegisterUser()
    {

        var ctx = new MockLunaLootMasterDbContext();
        var m = new MockUserManager(ctx);
        var passwordHasher = m.GetUserManager().PasswordHasher;
        
        
        var user = new ApplicationUser()
        {
            FirstName = "jhown",
            LastName = "doe",
            RefreshTokens = new string[]{}
        };
        user.PasswordHash =  passwordHasher.HashPassword(user, "Test!12");
        var res =  await m.GetUserManager().CreateAsync(user);

        res.Succeeded.Should().Be(true);
        res.Errors.Should().HaveCount(0);
        res.Errors.Should().BeEmpty();
        ctx.Dispose();
        m  .Dispose();
    }


    [Fact]
    public async void ShouldListUsers()
    {
        var ctx = new MockLunaLootMasterDbContext();
        var users = UsersStubs.UserStubs();
        var m = new MockUserManager(ctx);
         m.AddMockUsers(users);
        var dbUses =  m.GetUserManager().Users;
        dbUses.Should().NotBeNull();
        m.GetUserManager().Users.Count().Should().Be(2);
        ctx.Dispose();
        m  .Dispose();
      
    }

    [Fact]
    public async void ShouldGetUserByEmail()
    { 
        var ctx = new MockLunaLootMasterDbContext();
        var m = new MockUserManager(ctx);
        await m.AddMockUsers(UsersStubs.UserStubs());
        ApplicationUser result = ( await m.GetUserManager().FindByEmailAsync(UsersStubs.UserStubs()[0].Email!))!;

        result.Email.Should().Be(UsersStubs.UserStubs()[0].Email!);
        ctx.Dispose();
        m  .Dispose();
    }


    [Fact]
    public async void ShouldAddRoleToUser()
    {
        var ctx = new MockLunaLootMasterDbContext();
        var roleManager = new MockRoleManager(ctx);
        var userManager = new MockUserManager(ctx);
        await roleManager.GetManager().CreateAsync(new IdentityRole<Guid>("TestRole"));
        await userManager.GetUserManager().CreateAsync(UsersStubs.UserStubs()[0]);
        roleManager.GetManager().Logger.Log(LogLevel.Debug,"test");
        var user = await userManager.GetUserManager().FindByEmailAsync(UsersStubs.UserStubs()[0].Email!);
        var result = await userManager.GetUserManager().AddToRoleAsync(user!, "TestRole");
        result.Succeeded.Should().Be(true);
        
        ctx.Dispose();
        roleManager.Dispose();
        userManager.Dispose();
    }
    
    [Fact]
    public async void ShouldListUsersByRole()
    {
        var ctx = new MockLunaLootMasterDbContext();
        var roleManager = new MockRoleManager(ctx);
        var userManager = new MockUserManager(ctx);
        await userManager.AddMockUsers(UsersStubs.UserStubs());
        await roleManager.GetManager().CreateAsync(new IdentityRole<Guid>("TestRole"));

        

        var count = userManager.GetUserManager().Users.Count();

        count.Should().Be(2);
        var user = await userManager.GetUserManager().FindByEmailAsync(UsersStubs.UserStubs()[0].Email!);
        
        user.Should().NotBeNull();

        await userManager.GetUserManager().AddToRoleAsync(user!, "TestRole");
        
        var usersInRole = await userManager.GetUserManager().GetUsersInRoleAsync("TestRole");

 
        usersInRole.Should().NotBeNullOrEmpty();
        usersInRole.Count.Should().Be(1);
       
        ctx        .Dispose();
        userManager.Dispose();
        roleManager.Dispose();
    }

    [Fact]
    public async void ShouldRemoveRoleFromUser()
    {
        var ctx = new MockLunaLootMasterDbContext();
        var roleManager = new MockRoleManager(ctx);
        var userManager = new MockUserManager(ctx);
        await userManager.AddMockUsers(UsersStubs.UserStubs());
        await roleManager.GetManager().CreateAsync(new IdentityRole<Guid>("TestRole"));
        var user = await userManager.GetUserManager().FindByEmailAsync(UsersStubs.UserStubs()[0].Email!);
        
        // add role to user
        await userManager.GetUserManager().AddToRoleAsync(user!, "TestRole");
        
        // ensure is in role
        bool u = await userManager.GetUserManager().IsInRoleAsync(user!, "TestRole");
        u.Should().Be(true);
        
        // remove role from user
        await userManager.GetUserManager().RemoveFromRoleAsync(user!, "TestRole");
        
        // ensure is not in role
        u = await userManager.GetUserManager().IsInRoleAsync(user!, "TestRole");

      
        u.Should().Be(false);
        ctx        .Dispose();
        userManager.Dispose();
        roleManager.Dispose();
        
        
        
    }

    
    [Fact]
    public async void ShouldTakeUserAggregateAsArg()
    {
        var ctx = new MockLunaLootMasterDbContext();
        var userManager = new MockUserManager(ctx);
        var passwordHasher = new PasswordHasher<User>();
        var user = new User(UserId.Create(), "john", "doe", "jdoe@example.com", string.Empty, "12345", new List<RoleId>());

        user.PasswordHash = passwordHasher.HashPassword(user, "1234");

        var result = await userManager.GetUserManager().CreateAsync(user);

        result.Succeeded.Should().Be(true);
        
        
        ctx.Dispose();
        userManager.Dispose();
    }
    
}