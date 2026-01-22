using Surreal.Client.Rest.Model;

namespace Surreal.Client.Rest;

public interface ISurrealRestClient : IDisposable
{
    public Uri Uri { get; }
    
    public Task<bool> Status(CancellationToken cancellationToken = default);
    
    public Task<bool> Health(CancellationToken cancellationToken = default);

    public Task<string> Version(CancellationToken cancellationToken = default);

    public Task<bool> SignIn(CancellationToken cancellationToken = default);

    public Task<IEnumerable<Response<T>>> Get<T>(string table, CancellationToken cancellationToken = default);
    
    public Task<T> Get<T>(string table, string id, CancellationToken cancellationToken = default);

    public Task<T> Add<T>(string table, T record, CancellationToken cancellationToken = default);

    public Task<bool> Delete(string table, string id, CancellationToken cancellationToken = default);

    public Task<bool> Delete(string table, CancellationToken cancellationToken = default);
    
    public Task<T> Update<T>(string table, T record, CancellationToken cancellationToken = default);

    public Task<T> Query<T>(string query, IEnumerable<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default);
}