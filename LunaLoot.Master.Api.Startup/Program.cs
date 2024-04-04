

using LunaLoot.Api;
using LunaLoot.Master.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var currentDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent;
var app = new LunaLootApi(args, (services, config) =>
{
    config
        .AddJsonFile($"{currentDir}\\appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"{currentDir}\\appsettings.dev.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
    services.AddSingleton(config);
    services.AddLunaLootMasterApplication(config);
});



await app.RunAsync();