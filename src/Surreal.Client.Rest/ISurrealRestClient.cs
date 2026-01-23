using Surreal.Client.Rest.Model;

namespace Surreal.Client.Rest;

public interface ISurrealRestClient : IDisposable
{
    public Uri Uri { get; }
    
    public Task<SurrealHttpResponse<bool>> Status(CancellationToken cancellationToken = default);
    
    public Task<SurrealHttpResponse<bool>> Health(CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<string>> Version(CancellationToken cancellationToken = default);

    public Task<SurrealHttpArrayResponse<T>> Get<T>(string table, CancellationToken cancellationToken = default);
    
    public Task<SurrealHttpResponse<T>> Get<T>(string table, string id, CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<T>> Add<T>(string table, T record, CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<bool>> Delete(string table, string id, CancellationToken cancellationToken = default);

    public Task<SurrealHttpResponse<bool>> Delete(string table, CancellationToken cancellationToken = default);
    
    public Task<SurrealHttpResponse<T>> Update<T>(string table, T record, CancellationToken cancellationToken = default);

    public Task<SurrealHttpArrayResponse<T>> Query<T>(string query, IEnumerable<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);
}