using System.Text.Json;

namespace Surreal.Client.Rest;

public interface IResponseProcessor
{
    Task<SurrealHttpArrayResponse<T>> ProcessArrayResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default);

    Task<SurrealHttpResponse<T>> ProcessResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default);

    SurrealHttpResponse<bool> ProcessBoolResponse(HttpResponseMessage response, string? error = null);
}
