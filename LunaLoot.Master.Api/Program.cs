namespace LunaLoot.Api;

public class LunaLootApi
{

    private readonly WebApplication _app;
    public LunaLootApi(string[] args, Action<IServiceCollection, ConfigurationManager> options)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        options.Invoke(builder.Services, builder.Configuration);
        
        _app = builder.Build();
        _app.MapGet("/", () => "Hello World!");
    }

    public async Task RunAsync()
    {
        await _app.RunAsync();
    }
}