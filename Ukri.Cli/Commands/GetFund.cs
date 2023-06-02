using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Ukri.Cli.Api;

namespace Ukri.Cli.Commands;

[Verb("fund:get", HelpText = "Get a specific fund")]
public class GetFund : Command
{

    [Option('i', Required = true)] 
    public string Id { get; set; }

    protected override async Task ExecuteImplAsync(IServiceProvider serviceProvider)
    {
        using var apiClient = serviceProvider.GetService<ApiClient>();

        var fund = await apiClient.GetFundAsync(Id);
        
        Log.Information("Fund: {@Fund}", fund);
    }
}