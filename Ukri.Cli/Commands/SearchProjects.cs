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

        var size = 100;
        var page = 0;
        var projects = new List<Project>();
        var searchProjectsResponse = new SearchProjectsResponse();
        do
        {
            page += 1;
            
            var response = await httpClient.GetAsync($"/gtr/api/projects?q={Query}&s={size}&p={page}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            searchProjectsResponse = JsonConvert.DeserializeObject<SearchProjectsResponse>(json);
            Log.Information("Pagination: {@Pagination}", new { searchProjectsResponse.Page, searchProjectsResponse.TotalPages});
            projects.AddRange(searchProjectsResponse.Projects);
        } while (page != searchProjectsResponse.TotalPages);
        
        Log.Information("Projects: {@Projects}", projects);
    }
}