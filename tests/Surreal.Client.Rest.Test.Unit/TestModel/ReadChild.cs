using Surreal.Client.Rest.Metadata;
using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Unit.TestModel;

[Table("child")]
public class ReadChild : ModelBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("parent")]
    public Parent Parent { get; set; }
}

