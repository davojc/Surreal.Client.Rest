using System.Text.Json;

namespace Surreal.Client.Rest;

/// <summary>
/// You should have no reason to implement this yourself. It is here to help with Logging and DI. 
/// </summary>
public interface IResponseProcessor
{
    Task<SurrealHttpArrayResponse<T>> ProcessArrayResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default);

    Task<SurrealHttpResponse<T>> ProcessResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default);

    SurrealHttpResponse<bool> ProcessBoolResponse(HttpResponseMessage response, string? error = null);
}
