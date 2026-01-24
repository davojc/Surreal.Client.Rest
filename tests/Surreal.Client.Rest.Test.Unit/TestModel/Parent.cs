using Surreal.Client.Rest.Metadata;
using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Unit.TestModel;

[Table("parent")]
public class Parent : ModelBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

