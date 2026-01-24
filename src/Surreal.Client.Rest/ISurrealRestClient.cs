namespace Surreal.Client.Rest;

/// <summary>
/// The main SurrealRestClient interface. It is not one to one with the SurrealDB Client as that exposes more than the endpoints.
/// </summary>
public interface ISurrealRestClient : IDisposable
{
    public Uri Uri { get; }
    
    public Task<SurrealHttpResponse<bool>> Status(CancellationToken cancellationToken = default);
    
    public Task<SurrealHttpResponse<bool>> Health(CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<string>> Version(CancellationToken cancellationToken = default);

    public Task<SurrealHttpArrayResponse<T>> Get<T>(CancellationToken cancellationToken = default);
    
    public Task<SurrealHttpResponse<T>> Get<T>(string id, CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<T>> Add<T>(T record, CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<bool>> Delete<T>(string id, CancellationToken cancellationToken = default);
    
    public Task<SurrealHttpResponse<T>> Update<T>(T record, CancellationToken cancellationToken = default);

    public Task<SurrealHttpArrayResponse<T>> Query<T>(string query, IEnumerable<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);
}