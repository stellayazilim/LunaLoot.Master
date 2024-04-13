using FluentAssertions;
using LunaLoot.Infrastructure.UnitTests.Features.Auth.__mocks__;
using LunaLoot.Infrastructure.UnitTests.Features.Auth.Commands.Register.Extensions;
using LunaLoot.Infrastructure.UnitTests.Features.Auth.Commands.Register.Utils;
using LunaLoot.Infrastructure.UnitTests.Features.Auth.Utils;
using LunaLoot.Master.Application.Auth.Commands.Register;
using LunaLoot.Master.Infrastructure.Auth.Common.Providers;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit.Abstractions;

namespace LunaLoot.Infrastructure.UnitTests.Features.Auth.Commands.Register.Tests;

public class RegisterCommandTests
{
   private readonly ITestOutputHelper _testOutputHelper;


   private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;


   public RegisterCommandTests(ITestOutputHelper testOutputHelper)
   {
      _testOutputHelper = testOutputHelper;

      _mockUserManager = __Mocks__.MockUserManager(
         AuthUtils.CreateUserRange(10).ToList()
         );
   }

   
   /// <summary>
   /// Testing RegisterCommandHandler.Handle()
   /// Should create an instance of ApplicationUser
   /// then should call UserManager.CreateAsync with the instance
   /// and command.Password
   /// </summary>
   /// <param name="command"></param>
   /// <param name="expectedUser"></param>
   [Theory]
   [MemberData(nameof(CreateValidRegisterCommandsInRange), [5])]
   public async void RegisterCommandHandler_WhenUserIsValid_ShouldCreateUser(RegisterCommand command,  ApplicationUser expectedUser)
   {
      
      // arrange
      #region Arrange
      var handler = new RegisterCommandHandler(
         _mockUserManager.Object, 
         new ApplicationPasswordHasher());
      #endregion 
      
      // act
      #region act
      var result = await handler.Handle(command, default);
      #endregion
      
      // assert
      #region asssert
      result.IsError.Should().BeFalse();
      _mockUserManager.Verify(m => m.CreateAsync(
         It.Is<ApplicationUser>( user => user.CompareUserFrom(expectedUser)),
         expectedUser.PasswordHash!), Times.Once);
      #endregion
     

   }


   [Fact]
   public async void RegisterCommandHandler_WhenDuplicateEmail_ShouldFailCreating()
   {
      // arrange 
      #region arrange

         var command = RegisterCommandUtils.CreateCommand(null);
      
         var handler = new RegisterCommandHandler(
            _mockUserManager.Object, 
            new ApplicationPasswordHasher());
         
         _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(
               IdentityResult.Failed(
                   [new IdentityErrorDescriber().DuplicateEmail(command.Email)]
                  )
               ).Verifiable();
      #endregion
      
      // act
      #region act

         var result = await handler.Handle(command, default);
      #endregion
      
      // assert

      #region assert

      result.IsError.Should().BeTrue();
      result.Errors.FirstOrDefault().Code.Should().Be("DuplicateEmail");

      #endregion
   }
   
   /// <summary>
   ///   creates command, and expected user n times on given amount
   /// </summary>
   /// <param name="n"> count of results </param>
   /// <returns>
   ///  [registerCommand:typeof(RegisterCommand), expectedUser:typeof(ApplicationUser)]
   /// </returns>
    public static IEnumerable<object[]> CreateValidRegisterCommandsInRange(int n = 5)
   {
      foreach (var (registerCommand, index)  in RegisterCommandUtils.CreateCommandInRange(n).Select((v, i) =>(v, i)))
      {
         ApplicationUser expectedUser = AuthUtils.CreateUser(index);
         yield return [registerCommand,  expectedUser];
      }
      
   }
    
    
}