using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukri.Cli.Api;
using Ukri.Cli.GraphDb;

namespace Ukri.Cli;

public static class ServiceProviderFactory
{
    public static IServiceProvider Create()
    {
        var configuration = GetConfigurationRoot();

        return new ServiceCollection()
            .AddOptions()
            .AddApiClient()
            .AddGraphClient(configuration)
            .BuildServiceProvider();
    }

    private static IConfigurationRoot GetConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, false)
            .AddEnvironmentVariables()
            .AddUserSecrets<Command>()
            .Build();
    }
}