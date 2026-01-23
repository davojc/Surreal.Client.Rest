using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Integration.TestModel;

public class Parent : ModelBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public abstract class ModelBase
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
