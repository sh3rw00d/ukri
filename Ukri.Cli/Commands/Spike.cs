using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Ukri.Cli.GraphDb;
using Ukri.Cli.GraphDb.Model;

namespace Ukri.Cli.Commands;

[Verb("spike")]
public class Spike : Command
{
    protected override async Task ExecuteImplAsync(IServiceProvider serviceProvider)
    {
        var graphClient = serviceProvider.GetService<GraphClient>();

        var v1 = await graphClient.CreateAsync(new Search
        {
            Term = "Cake"
        });
        Log.Information("Query: {@query}", v1);
        
        var v2 = await graphClient.CreateAsync(new Search
        {
            Term = "Cake"
        });
        
        Log.Information("Query: {@query}", v2);
    }
}