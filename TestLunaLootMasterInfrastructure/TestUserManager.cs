using __mocks__;
using __stubs__;
using FluentAssertions;
using LunaLoot.Master.Domain.User;
using LunaLoot.Master.Domain.User.ValueObjects;
using LunaLoot.Master.Infrastructure.Entities;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace TestLunaLootMasterInfrastructure;


public class TestUserManager
{

    public UserManager<ApplicationUser> _sut;
    private readonly Mock<IUserStore<ApplicationUser>> _mockUserStore = new();
    private readonly Mock<IOptions<IdentityOptions>> _mockOptions = new();
    private readonly Mock<IPasswordHasher<ApplicationUser>> _mockPasswordHasher = new();
    private readonly Mock<IEnumerable<IUserValidator<ApplicationUser>>> _mockUserValidators = new();
    private readonly Mock<IEnumerable<IPasswordValidator<ApplicationUser>>> _mockPasswordValidator = new();
    private readonly Mock<ILookupNormalizer> _mockLookupNormalizer = new();
    private readonly Mock<IdentityErrorDescriber> _mockErrorDescriber = new();
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();
    private readonly Mock<ILogger<UserManager<ApplicationUser>>> _mockLogger = new();
    
    public TestUserManager()
    {
        
        
       
        // _sut = new UserManager<ApplicationUser>(
        //     _mockUserStore.Object,
        //     _mockOptions.Object,
        //     _mockPasswordHasher.Object,
        //     _mockUserValidators.Object,
        //     _mockPasswordValidator.Object,
        //     _mockLookupNormalizer.Object,
        //     _mockErrorDescriber.Object,
        //     _mockServiceProvider.Object,
        //     _mockLogger.Object
        // );
    }
    
    
    
}