using System.Net;
using System.Text.Json;
using Surreal.Client.Rest.Model;

namespace Surreal.Client.Rest;

internal static class ResponseExtensions
{
    public static async Task<SurrealHttpArrayResponse<T>> ProcessArrayResponse<T>(this HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return SurrealHttpArrayResponse<T>.Failure("Not authenticated, sign back in.", response.StatusCode);
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return SurrealHttpArrayResponse<T>.Failure("Failed to get response", response.StatusCode);
        }
        
        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var responseObject = await JsonSerializer.DeserializeAsync<Response<T>[]>(responseStream, cancellationToken: cancellationToken);

        if (responseObject == null)
        {
            return SurrealHttpArrayResponse<T>.Failure("Failed to deserialise response", HttpStatusCode.NotFound);
        }
        
        return SurrealHttpArrayResponse<T>.Success(responseObject.First().Result, response.StatusCode);
    }
    
    public static async Task<SurrealHttpResponse<T>> ProcessResponse<T>(this HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return SurrealHttpResponse<T>.Failure("Not authenticated.", response.StatusCode);
        }
        
        if (!response.IsSuccessStatusCode)
        {
            return SurrealHttpResponse<T>.Failure("Failed to get response", response.StatusCode);
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        var responseObject = JsonSerializer.Deserialize<Response<T>[]>(responseString);


        //using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        //var responseObject = await JsonSerializer.DeserializeAsync<Response<T>[]>(responseStream, cancellationToken: cancellationToken);

        if (responseObject == null)
        {
            return SurrealHttpResponse<T>.Failure("Failed to deserialise response", HttpStatusCode.NotFound);
        }
        
        return SurrealHttpResponse<T>.Success(responseObject.First().Result.First(), response.StatusCode);
    }

    public static SurrealHttpResponse<bool> ProcessBoolResponse(this HttpResponseMessage response, string? error = null,
        CancellationToken cancellationToken = default)
    {
        if (response.IsSuccessStatusCode)
        {
            return SurrealHttpResponse<bool>.Success(true, response.StatusCode);
        }

        return SurrealHttpResponse<bool>.Failure(error ?? "Status code was not OK,", response.StatusCode);
    }
}
