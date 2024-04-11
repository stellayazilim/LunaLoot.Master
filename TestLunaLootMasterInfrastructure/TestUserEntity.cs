using FluentAssertions;
using LunaLoot.Master.Domain.User;
using LunaLoot.Master.Domain.User.ValueObjects;
using LunaLoot.Master.Infrastructure.Entities;

namespace TestLunaLootMasterInfrastructure;

public class TestUserEntity
{

    [Fact]
    public void ShouldConvertedToAggregate()
    {
        var userModel = new ApplicationUser()
        {
            FirstName = "jhon",
            LastName = "doe",
            Email = "jhon@doe.com",
            PasswordHash = "1234",
            RefreshTokens = new string[]{}

        };


        var domainUser = userModel.AsUserAggregate();

        domainUser.Id.Value.Should().Be(userModel.Id);
        domainUser.FirstName.Should().Be(userModel.FirstName);
        domainUser.LastName.Should().Be(userModel.LastName);
    }

    [Fact]
    public void ShouldExpilictlyConvertedFromApplicationUser()
    {

        var userAggregate = new User(
                UserId.Create(),
                "john",
                "doe",
                "jdoe@example.com",
                "1234",
                "1234",
                new List<RoleId>()
            );


        ApplicationUser userModel = userAggregate;

        userModel.FirstName.Should().Be(userAggregate.FirstName);
        userModel.LastName.Should().Be(userAggregate.LastName);
        userModel.Email.Should().Be(userAggregate.Email);
    }
}