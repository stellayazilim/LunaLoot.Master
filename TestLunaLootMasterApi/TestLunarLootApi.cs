using FluentAssertions;
using FluentAssertions.Execution;
using LunaLoot.Api;
using LunaLoot.Api.Startup;


namespace TestLunaLootMasterApi;

public class TestLunarLootApi
{
    [Fact]
    public void ApiMustBeDependedOnStartup()
    {

        typeof(ILunaLootApiStartupAssemblyReference).Assembly
            .Should().Reference(typeof(LunaLootApi).Assembly);
    }

    [Fact]
    public void StartupMustBeNotDependedOnApi()
    {
        typeof(LunaLootApi)
            .Assembly
            .Should()
            .NotReference(typeof(ILunaLootApiStartupAssemblyReference).Assembly);
    }
}