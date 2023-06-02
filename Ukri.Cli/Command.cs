using CommandLine;
using Serilog;
using Serilog.Events;

namespace Ukri.Cli;

public abstract class Command
{
    [Option('l', "LogLevel", Required = false, Default = LogEventLevel.Debug)]
    public LogEventLevel LogLevel { get; set; }
    
    protected abstract Task ExecuteImplAsync();
    
    public async Task ExecuteAsync()
    {
        InitialiseLogging();

        Log.Information("Executing {@Command}", this);

        try
        {
            await ExecuteImplAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred executing command");
        }
    }

    private void InitialiseLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Is(LogLevel)
            .CreateLogger();
    }
    
}