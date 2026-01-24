using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Unit.TestModel;

public abstract class ModelBase
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

