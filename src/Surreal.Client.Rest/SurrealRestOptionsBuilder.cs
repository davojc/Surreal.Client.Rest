namespace Surreal.Client.Rest;

public sealed class SurrealRestOptionsBuilder
{
    private string? _endpoint;
    private string? _namespace;
    private string? _database;
    private string? _username;
    private string? _password;
    
    public SurrealRestOptionsBuilder WithEndpoint(string? endpoint)
    {
        _endpoint = endpoint;
        return this;
    }

    public SurrealRestOptionsBuilder WithNamespace(string? ns)
    {
        _namespace = ns;
        return this;
    }

    public SurrealRestOptionsBuilder WithDatabase(string? db)
    {
        _database = db;
        return this;
    }

    public SurrealRestOptionsBuilder WithUsername(string? username)
    {
        _username = username;
        return this;
    }

    public SurrealRestOptionsBuilder WithPassword(string? password)
    {
        _password = password;
        return this;
    }

    public SurrealRestOptions Build()
    {
        return new SurrealRestOptions
        {
            Endpoint = _endpoint,
            Namespace = _namespace,
            Database = _database,
            Username = _username,
            Password = _password,
        };
    }
}