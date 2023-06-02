using Microsoft.Extensions.DependencyInjection;

namespace Ukri.Cli.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection self) => self.AddSingleton<ApiClient>();
}