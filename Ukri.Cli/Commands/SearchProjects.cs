using CommandLine;
using Serilog;
using Ukri.Cli.Api;

namespace Ukri.Cli.Commands;

[Verb("projects:search", HelpText = "Search projects for a given term")]
public class SearchProjects : Command
{
    [Option('q', Required = true)] 
    public string Query { get; set; }

    protected override async Task ExecuteImplAsync()
    {
        using var apiClient = new ApiClient();

        var projects = await apiClient.SearchProjectsAsync(Query);
        
        Log.Information("Projects: {@Projects}", projects);
    }
}