using CommandLine;
using Ukri.Cli.Commands;

namespace Ukri.Cli;

public static class Program
{
    private static int Main(string[] args) {
        return Parser.Default.ParseArguments<Spike, SearchProjects, GetFund>(args)
            .MapResult(
                (Spike command) => Execute(command),
                (SearchProjects command) => Execute(command),
                (GetFund command) => Execute(command),
                _ => 1);
    }

    private static int Execute(Command command)
    {
        command.ExecuteAsync().Wait();
        return 0;
    }
}