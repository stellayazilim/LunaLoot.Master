using System.Text;
using Microsoft.Extensions.Configuration;

namespace __mocks__;

public class MockConfigurationManager
{
    public static ConfigurationManager GetConfig()
    {
        var currentDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent;

        var manager = new ConfigurationManager();
            manager.AddJsonFile($"{currentDir}\\appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{currentDir}\\appsettings.dev.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        
        return manager;
    }
}