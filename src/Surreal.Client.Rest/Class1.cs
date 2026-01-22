using SurrealDb.Net;
using SurrealDb.Net.Handlers;
using SurrealDb.Net.Models;
using SurrealDb.Net.Models.Auth;
using SurrealDb.Net.Models.LiveQuery;
using SurrealDb.Net.Models.Response;
using SystemTextJsonPatch;

namespace Surreal.Client.Http;

public interface ISurrealRestClient
{
    
}

public class Class1 : ISurrealDbClient
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task Authenticate(Jwt jwt, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Connect(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T> Create<T>(T data, CancellationToken cancellationToken = new CancellationToken()) where T : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<T> Create<T>(string table, T? data = default(T?), CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Create<TData, TOutput>(StringRecordId recordId, TData? data = default(TData?),
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task Delete(string table, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(RecordId recordId, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(StringRecordId recordId, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<string> Export(ExportOptions? options = null, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<bool> Health(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Import(string input, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T> Info<T>(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Insert<T>(string table, IEnumerable<T> data, CancellationToken cancellationToken = new CancellationToken()) where T : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<T> InsertRelation<T>(T data, CancellationToken cancellationToken = new CancellationToken()) where T : IRelationRecord
    {
        throw new NotImplementedException();
    }

    public Task<T> InsertRelation<T>(string table, T data, CancellationToken cancellationToken = new CancellationToken()) where T : IRelationRecord
    {
        throw new NotImplementedException();
    }

    public Task Invalidate(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Kill(Guid queryUuid, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public SurrealDbLiveQuery<T> ListenLive<T>(Guid queryUuid)
    {
        throw new NotImplementedException();
    }

    public Task<SurrealDbLiveQuery<T>> LiveQuery<T>(QueryInterpolatedStringHandler query, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SurrealDbLiveQuery<T>> LiveRawQuery<T>(string query, IReadOnlyDictionary<string, object?>? parameters = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SurrealDbLiveQuery<T>> LiveTable<T>(string table, bool diff = false, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Merge<TMerge, TOutput>(TMerge data, CancellationToken cancellationToken = new CancellationToken()) where TMerge : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<T> Merge<T>(RecordId recordId, Dictionary<string, object> data, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T> Merge<T>(StringRecordId recordId, Dictionary<string, object> data, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Merge<TMerge, TOutput>(string table, TMerge data, CancellationToken cancellationToken = new CancellationToken()) where TMerge : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Merge<T>(string table, Dictionary<string, object> data, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T> Patch<T>(RecordId recordId, JsonPatchDocument<T> patches,
        CancellationToken cancellationToken = new CancellationToken()) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Patch<T>(StringRecordId recordId, JsonPatchDocument<T> patches,
        CancellationToken cancellationToken = new CancellationToken()) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Patch<T>(string table, JsonPatchDocument<T> patches, CancellationToken cancellationToken = new CancellationToken()) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<SurrealDbResponse> Query(QueryInterpolatedStringHandler query, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<SurrealDbResponse> RawQuery(string query, IReadOnlyDictionary<string, object?>? parameters = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Relate<TOutput>(string table, RecordId @in, RecordId @out,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Relate<TOutput, TData>(string table, RecordId @in, RecordId @out, TData? data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Relate<TOutput>(string table, IEnumerable<RecordId> ins, RecordId @out,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Relate<TOutput, TData>(string table, IEnumerable<RecordId> ins, RecordId @out, TData? data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Relate<TOutput>(string table, RecordId @in, IEnumerable<RecordId> outs,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Relate<TOutput, TData>(string table, RecordId @in, IEnumerable<RecordId> outs, TData? data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Relate<TOutput>(string table, IEnumerable<RecordId> ins, IEnumerable<RecordId> outs,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Relate<TOutput, TData>(string table, IEnumerable<RecordId> ins, IEnumerable<RecordId> outs, TData? data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Relate<TOutput>(RecordId recordId, RecordId @in, RecordId @out,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Relate<TOutput, TData>(RecordId recordId, RecordId @in, RecordId @out, TData? data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Run<T>(string name, object[]? args = null, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T> Run<T>(string name, string version, object[]? args = null,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Select<T>(string table, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T?> Select<T>(RecordId recordId, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T?> Select<T>(StringRecordId recordId, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Select<TStart, TEnd, TOutput>(RecordIdRange<TStart, TEnd> recordIdRange,
        CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task Set(string key, object value, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task SignIn(RootAuth root, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<Jwt> SignIn(NamespaceAuth nsAuth, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<Jwt> SignIn(DatabaseAuth dbAuth, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<Jwt> SignIn<T>(T scopeAuth, CancellationToken cancellationToken = new CancellationToken()) where T : ScopeAuth
    {
        throw new NotImplementedException();
    }

    public Task<Jwt> SignUp<T>(T scopeAuth, CancellationToken cancellationToken = new CancellationToken()) where T : ScopeAuth
    {
        throw new NotImplementedException();
    }

    public Task Unset(string key, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<T> Update<T>(T data, CancellationToken cancellationToken = new CancellationToken()) where T : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Update<TData, TOutput>(StringRecordId recordId, TData data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Update<T>(string table, T data, CancellationToken cancellationToken = new CancellationToken()) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Update<TData, TOutput>(string table, TData data, CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Update<TData, TOutput>(RecordId recordId, TData data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<T> Upsert<T>(T data, CancellationToken cancellationToken = new CancellationToken()) where T : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Upsert<TData, TOutput>(StringRecordId recordId, TData data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> Upsert<T>(string table, T data, CancellationToken cancellationToken = new CancellationToken()) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TOutput>> Upsert<TData, TOutput>(string table, TData data, CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task<TOutput> Upsert<TData, TOutput>(RecordId recordId, TData data,
        CancellationToken cancellationToken = new CancellationToken()) where TOutput : IRecord
    {
        throw new NotImplementedException();
    }

    public Task Use(string ns, string db, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Task<string> Version(CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public Uri Uri { get; }
    public string? NamingPolicy { get; }
}
