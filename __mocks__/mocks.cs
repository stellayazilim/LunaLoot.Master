using LunaLoot.Master.Infrastructure.Entities;
using LunaLoot.Master.Infrastructure.Persistence.EFCore.Context;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace __mocks__;

public static class Mocks
{
    public static Mock<UserManager<ApplicationUser>> MockUserManager = new Mock<UserManager<ApplicationUser>>();
    public static Mock<RoleManager<IdentityRole<Guid>>> MockRoleManager = new Mock<RoleManager<IdentityRole<Guid>>>();
    public static Mock<LunaLootMasterDbContext> MockDbContext = new Mock<LunaLootMasterDbContext>();
}

