using System.Net.Http.Headers;
using CommandLine;
using Newtonsoft.Json;
using Serilog;
using Ukri.Cli.Api.Model;

namespace Ukri.Cli.Commands;

[Verb("projects:search", HelpText = "Search projects for a given term")]
public class SearchProjects : Command
{

    [Option('q', Required = true)] 
    public string? Query { get; set; }

    protected override async Task ExecuteImplAsync()
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://gtr.ukri.org"),
        };

        httpClient.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/vnd.rcuk.gtr.json-v7"));

        var response = await httpClient.GetAsync($"/gtr/api/projects?q={Query}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();

        var searchProjectsResponse = JsonConvert.DeserializeObject<SearchProjectsResponse>(json);
        
        Log.Information("Response: {@Response}", searchProjectsResponse);
    }
}