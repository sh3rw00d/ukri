using Neo4j.Driver;
using Ukri.Cli.GraphDb.Model;

namespace Ukri.Cli.GraphDb;

public class GraphClient
{
    private readonly IDriver _driver;

    public GraphClient(IDriver driver) => _driver = driver;
    
    public async Task<Search> CreateAsync(Search search)
    {
        await using var session = _driver.AsyncSession();
        
        return await session.ExecuteWriteAsync(async tx =>
        {
            var result = await tx.RunAsync("MERGE (search:Search {term: $Term}) RETURN ID(search) as id, search.term as term", search);

            var record = await result.SingleAsync();
            return new Search
            {
                Id = record["id"].As<string>(),
                Term = record["term"].As<string>()
            };
        });
    }
}