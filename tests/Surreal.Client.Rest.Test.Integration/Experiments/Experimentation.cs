using Surreal.Client.Rest.Serialisation;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

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






