using Neo4j.Driver;

namespace Ukri.Cli.GraphDb;

public class GraphClient
{
    private readonly IDriver _driver;

    public GraphClient(IDriver driver) => _driver = driver;

    public async Task<string> CreateGreetingAsync(string message)
    {
        await using var session = _driver.AsyncSession();
        
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync(
                "CREATE (a:Greeting) " +
                "SET a.message = $message " +
                "RETURN a.message + ', from node ' + id(a)",
                new { message });

            var record = await result.SingleAsync();
            return record[0].As<string>();
        });
    }
}