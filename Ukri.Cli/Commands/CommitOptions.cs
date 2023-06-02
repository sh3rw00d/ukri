using CommandLine;

namespace Ukri.Cli.Commands;

[Verb("commit", HelpText = "Record changes to the repository.")]
public class CommitOptions : Command {

    protected override Task ExecuteImplAsync()
    {
        throw new NotImplementedException();
    }
}