using Surreal.Client.Rest.Metadata;
using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Unit.TestModel;

[Table("child")]
public class WriteChild : ModelBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("parent")]
    [IdField(typeof(Parent))]
    public string Parent { get; set; }
}

