using CommandLine;
using Ukri.Cli.Commands;

namespace Ukri.Cli;

public static class Program
{
    private static int Main(string[] args) {
        return Parser.Default.ParseArguments<AddOptions, CommitOptions, CloneOptions>(args)
            .MapResult(
                (AddOptions command) => command.Execute(),
                (CommitOptions command) => command.Execute(),
                (CloneOptions command) => command.Execute(),
                _ => 1);
    }
}