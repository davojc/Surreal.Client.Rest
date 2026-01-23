using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Model;

internal class Response<T>
{
    [JsonPropertyName("result")]
    public T[] Result { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("time")]
    public string Time { get; set; }
}