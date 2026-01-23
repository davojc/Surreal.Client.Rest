using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Model;

public class Authentication
{
    [JsonPropertyName("code")]
    public int Code { get; set; }
    
    [JsonPropertyName("details")]
    public string Details { get; set; }
    
    
    [JsonPropertyName("token")]
    public string Token { get; set; }
    
    [JsonPropertyName("refresh")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public object Refresh { get; set; }
}