

using LunaLoot.Master.Api;
using LunaLoot.Master.Application;

var builder = WebApplication.CreateBuilder();

builder.Services.AddLunaLootMasterApplication(builder.Configuration);
builder.Services.ConfigureStartupServices();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

app.ConfigureWebApp();

app.Run();




