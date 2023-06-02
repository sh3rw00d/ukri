using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using Ukri.Cli.Graph;

namespace Ukri.Cli.GraphDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraphClient(this IServiceCollection self, IConfigurationRoot configuration)
    {
        return self
            .Configure<GraphDbSettings>(configuration.GetSection("Neo4j"))
            .AddSingleton(provider =>
            {
                var settings = provider.GetService<IOptions<GraphDbSettings>>().Value;

                return GraphDatabase.Driver(settings.Uri, AuthTokens.Basic(settings.Username, settings.Password));
            })
            .AddSingleton<GraphClient>();
    }
}