using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Ukri.Cli.GraphDb;

namespace Ukri.Cli.Commands;

[Verb("spike")]
public class Spike : Command
{

    protected override async Task ExecuteImplAsync(IServiceProvider serviceProvider)
    {
        var graphClient = serviceProvider.GetService<GraphClient>();

        var greeting = await graphClient.CreateGreetingAsync("Hello again");
        
        Console.WriteLine(greeting);
    }
}