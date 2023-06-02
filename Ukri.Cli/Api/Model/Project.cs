using Newtonsoft.Json;

namespace Ukri.Cli.Api.Model;

public class Project
{
    public string Id { get; set; }

    public string Title { get; set; }
    
    [JsonProperty("abstractText")]
    public string Abstract { get; set; }
    
    public string LeadFunder { get; set; }
    
    public string LeadOrganisationDepartment { get; set; }
}