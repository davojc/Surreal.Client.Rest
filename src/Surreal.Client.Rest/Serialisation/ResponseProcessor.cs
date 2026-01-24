using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Surreal.Client.Rest.Model;
using System.Net;
using System.Text.Json;

namespace Surreal.Client.Rest.Serialisation;


internal partial class ResponseProcessor(IOptions<SurrealRestOptions> clientOptions, ILogger<ResponseProcessor> logger) : IResponseProcessor
{
    private ILogger<ResponseProcessor> _logger = logger;

    public SurrealHttpResponse<bool> ProcessBoolResponse(HttpResponseMessage response, string? error = null)
    {
        if (response.IsSuccessStatusCode)
        {
            return SurrealHttpResponse<bool>.Success(true, response.StatusCode);
        }

        return SurrealHttpResponse<bool>.Failure(error ?? "Status code was not OK,", response.StatusCode);
    }

    public async Task<SurrealHttpArrayResponse<T>> ProcessArrayResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default)
    {
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return SurrealHttpArrayResponse<T>.Failure("Not authenticated, sign back in.", response.StatusCode);
        }

        if (!response.IsSuccessStatusCode)
        {
            return SurrealHttpArrayResponse<T>.Failure("Failed to get response", response.StatusCode);
        }

        var responseObject = await DeserialiseResponse<T>(response, options, cancellationToken);

        if (responseObject == null)
        {
            return SurrealHttpArrayResponse<T>.Failure("Failed to deserialise response", HttpStatusCode.NotFound);
        }

        return SurrealHttpArrayResponse<T>.Success(responseObject.First().Result, response.StatusCode);
    }

    public async Task<SurrealHttpResponse<T>> ProcessResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default)
    {
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return SurrealHttpResponse<T>.Failure("Not authenticated.", response.StatusCode);
        }

        if (!response.IsSuccessStatusCode)
        {
            return SurrealHttpResponse<T>.Failure("Failed to get response", response.StatusCode);
        }

        var responseObject = await DeserialiseResponse<T>(response, options, cancellationToken);

        if (responseObject == null)
        {
            return SurrealHttpResponse<T>.Failure("Failed to deserialise response", HttpStatusCode.NotFound);
        }

        return SurrealHttpResponse<T>.Success(responseObject.FirstOrDefault().Result.First(), response.StatusCode);
    }

    public async Task<Response<T>[]?> DeserialiseResponse<T>(HttpResponseMessage response, JsonSerializerOptions options, CancellationToken cancellationToken = default)
    {
        if (clientOptions.Value.Debug)
        {
            try
            {
                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

                LogProcessingDebug(responseString);

                return JsonSerializer.Deserialize<Response<T>[]>(responseString, options);
            }
            catch (Exception ex)
            {
                LogProcessingError(ex);
                throw;
            }
        }
        else
        {
            try
            {
                using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                return await JsonSerializer.DeserializeAsync<Response<T>[]>(responseStream, options, cancellationToken: cancellationToken);
            }
            catch(Exception ex)
            {
                LogProcessingError(ex);
                throw;
            }
        }
    }
}
