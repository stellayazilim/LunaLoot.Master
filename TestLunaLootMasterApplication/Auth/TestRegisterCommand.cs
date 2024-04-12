using Microsoft.AspNetCore.Identity;
using LunaLoot.Master.Application.Auth.Commands.Register;
using LunaLoot.Master.Infrastructure.Entities;
using FluentAssertions;
using MediatR;
using ErrorOr;
using Moq;
using __stubs__;
using LunaLoot.Master.Infrastructure.Auth.Common.Services;


namespace TestLunaLootMasterApplication.Auth;

public class TestRegisterCommand
{

    private readonly RegisterCommand registerCommand;
    private readonly RegisterCommandHandler _sut;
    private readonly Mock<UserManager<ApplicationUser>> mockUserManager = 
        new(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
    private readonly Mock<ApplicationPasswordHasher> mockPasswordHasher = new();
    private const string password = "12345678";
    private readonly ApplicationUser user = UsersStubs.UserStub;
    
    
    public TestRegisterCommand()
    {

        registerCommand = new RegisterCommand(
            user.FirstName,
            user.LastName,
            user.MobilePhoneNumber!,
            user.Email!,
            password
        );


        _sut = new RegisterCommandHandler(mockUserManager.Object, mockPasswordHasher.Object);
    }
    
    
    
    [Fact]
    public void ShouldImplementIRequest()
    {
        typeof(RegisterCommand).Should().Implement<IRequest<ErrorOr<RegisterCommandResult>>>();
    }

    [Fact]
    public void RegisterCommand_should_have_properties()
    {
        
        // arrange 
        var user = UsersStubs.UserStub;
        
        // assert 
        registerCommand.FirstName.Should().Be(user.FirstName);
        registerCommand.LastName.Should().Be(user.LastName);
        registerCommand.Email.Should().Be(user.Email);
        registerCommand.MobilePhoneNumber.Should().Be(user.MobilePhoneNumber);
        registerCommand.Password.Should().Be(password);
    }

    
    [Fact]
    public void RegisterCommand_should_successValidation()
    {
        
        // arrange
        var validator = new RegisterCommandValidator();

        // act
        var result = validator.Validate(registerCommand);
        
        // assert
        result.IsValid.Should().Be(true);
    }


    [Fact]
    public void RegiserCommand_should_failValidation()
    {
        // arrange 
        var validator = new RegisterCommandValidator();
        var command = new RegisterCommand(
            FirstName: "john",
            LastName: "doe",
            Email: "invalid email",
            Password: "12345",
            MobilePhoneNumber: "123456"
        );

       
        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().Be(false);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    
    [Fact]
    public async void should_register_user()
    {
        //  Arrange
        mockUserManager.Setup(
                x => x.CreateAsync(It.IsAny<ApplicationUser>())
            ).ReturnsAsync(IdentityResult.Success);
      
        
        // Act 
        var result = await _sut.Handle(registerCommand, CancellationToken.None);
        
        // Assert
        result.IsError.Should().Be(false);
    }

   
    
    [Fact]
    public async void Should_fail_on_duplicate_email()
    {
        
        // arrange 
        mockUserManager.Setup(
            x => x.CreateAsync(It.IsAny<ApplicationUser>())
        ).ReturnsAsync(IdentityResult.Failed(
            new IdentityError[]
            {
                new IdentityErrorDescriber().DuplicateEmail(registerCommand.Email)
            }));
        
        // act 
        var result = await _sut.Handle(registerCommand, CancellationToken.None);
        
        // assert
        result.IsError.Should().BeTrue();
        result.ErrorsOrEmptyList.Count.Should().Be(1);
      
    }


    
    [Fact]
    public async void should_return_list_of_identity_error()
    {
        // arrange 
        mockUserManager.Setup(
            x => x.CreateAsync(It.IsAny<ApplicationUser>())
        ).ReturnsAsync(IdentityResult.Failed(
            new IdentityError[] {
                new IdentityError {
                    Code = $"Email '{user.Email}' is already taken.",
                    Description = "error for duplicate emails" }}));
        
        // act 
        var handler = await _sut.Handle(registerCommand, CancellationToken.None);


        handler.IsError.Should().BeTrue();
        handler.ErrorsOrEmptyList.Count.Should().Be(1);

    }
}