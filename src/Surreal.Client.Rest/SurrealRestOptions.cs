namespace Surreal.Client.Rest;

[Flags]
public enum SurrealIdOptions
{
    None = 0,
    ExposeSurrealIds = 1,
    Optimise = 2
}

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

    /// <summary>
    /// This property determines how ids are treated. If set to ExposeSurrealIds then ids will return and expect the SurrealDb approach to ids (table:id). 
    /// If false (default) you can simply use ids and the Client will insert/remove the table name. Optimise optimises these transformations but cannot be used if the SurrealId is > 256 characters.
    /// </summary>
    public SurrealIdOptions SurrealIdOptions { get; set; } = SurrealIdOptions.None;

   
    public static SurrealRestOptionsBuilder Create()
    {
        return new SurrealRestOptionsBuilder();
    }
}