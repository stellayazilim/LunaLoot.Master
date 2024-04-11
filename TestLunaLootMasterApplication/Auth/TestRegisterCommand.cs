using __mocks__;
using __stubs__;
using LunaLoot.Master.Application.Auth.Commands.Register;
using FluentAssertions;
using MediatR;
using ErrorOr;
using LunaLoot.Master.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TestLunaLootMasterApplication.Auth;

public class TestRegisterCommand
{

    [Fact]
    public void ShouldImplementIRequest()
    {
        typeof(RegisterCommand).Should().Implement<IRequest<ErrorOr<RegisterCommandResult>>>();
    }

    
    
    
    [Fact]
    public async void ShouldRegisteruser()
    {

        var userStub = UsersStubs.UserStubs()[0]!;
        
        var ctx = new MockLunaLootMasterDbContext();
        var userManager = new MockUserManager(ctx);

        var handler = new RegisterCommandHandler(userManager.GetUserManager());


        var result = await handler.Handle(new RegisterCommand(
            Email: userStub.Email,
            FirstName: userStub.FirstName,
            LastName: userStub.LastName,
            MobilePhoneNumber: userStub.MobilePhoneNumber,
            Password: "1234"
        ), new CancellationToken());

        var res = await userManager.GetUserManager().FindByEmailAsync(userStub.Email);


        res.Email.Should().Be(userStub.Email);
        result.IsError.Should().Be(false);
        
        ctx.Dispose();
        userManager.Dispose();
    }


    [Fact]
    public async void ShouldReturnIdentityError()
    {
      

    }
}