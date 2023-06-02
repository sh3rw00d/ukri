using Newtonsoft.Json;

namespace Ukri.Cli.Api.Model;

public class SearchProjectsResponse
{
    public int Page { get; set; }
    
    public int Size { get; set; }
    
    public int TotalPages { get; set; }
    
    public int TotalSize { get; set; }

    [JsonProperty("Project")] 
    public Project[] Projects { get; set; } = Array.Empty<Project>();
}