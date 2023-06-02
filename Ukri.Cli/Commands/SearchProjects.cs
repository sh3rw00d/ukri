using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Ukri.Cli.Api;

namespace Ukri.Cli.Commands;

[Verb("projects:search", HelpText = "Search projects for a given term")]
public class SearchProjects : Command
{
    [Option('q', Required = true)] 
    public string Query { get; set; }

    protected override async Task ExecuteImplAsync(IServiceProvider serviceProvider)
    {
        using var apiClient = serviceProvider.GetService<ApiClient>();

        var projects = await apiClient.SearchProjectsAsync(Query);
        
        Log.Information("Projects: {@Projects}", projects);
    }
}