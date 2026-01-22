using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Model;

internal class SignIn
{
    [JsonPropertyName("ns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Namespace { get; set; }
    
    [JsonPropertyName("db")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Database { get; set; }
    
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Username { get; set; }
    
    [JsonPropertyName("pass")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Password { get; set; }
}

internal static class Extensions
{
    public static SignIn CreateSignIn(this SurrealRestOptions option)
    {
        return new SignIn
        {
            Username = option.Username,
            Password = option.Password
        };
    }
}