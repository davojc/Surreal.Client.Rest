using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Integration.TestModel;

public class WriteChild : ModelBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("parent")]
    public string Parent { get; set; }
}

public class ReadChild : ModelBase
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("parent")]
    public Parent Parent { get; set; }
}