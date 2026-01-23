using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Integration;

public class Parent
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
