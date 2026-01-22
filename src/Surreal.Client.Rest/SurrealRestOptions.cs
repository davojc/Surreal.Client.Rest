namespace Surreal.Client.Rest;

public sealed class SurrealRestOptions
{
    public SurrealRestOptions()
    {
    }
    
    public SurrealRestOptions(SurrealRestOptions clone)
    {
        Endpoint = clone.Endpoint;
        Namespace = clone.Namespace;
        Database = clone.Database;
        Username = clone.Username;
        Password = clone.Password;
    }
    
    public string? Endpoint { get; set; }
    
    public string? Namespace { get; set; }

    public string? Database { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
    
    public static SurrealRestOptionsBuilder Create()
    {
        return new SurrealRestOptionsBuilder();
    }
}