using Surreal.Client.Rest.Metadata;
using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Integration.Experiments;

public class Experimentation
{

}

[Table("model")]
public class Model
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}






