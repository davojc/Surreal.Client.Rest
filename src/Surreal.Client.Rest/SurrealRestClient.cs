using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Surreal.Client.Rest.Serialisation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Surreal.Client.Rest;

public partial class SurrealRestClient : ISurrealRestClient
{
    private readonly HttpClient client;
    private readonly IOptions<SurrealRestOptions> options;
    private readonly ILogger<SurrealRestClient> logger;
    private readonly IResponseProcessor responseProcessor;
    private readonly JsonSerializerOptions jsonOptions;

    public SurrealRestClient(HttpClient client, IOptions<SurrealRestOptions> options, IResponseProcessor responseProcessor, ILogger<SurrealRestClient> logger)
    {
        this.client = client;
        this.options = options;
        this.logger = logger;
        this.responseProcessor = responseProcessor;
        Uri = new Uri(options.Value.Endpoint);

        if (options.Value.SurrealIdOptions.HasFlag(SurrealIdOptions.ExposeSurrealIds))
        {
            jsonOptions = new JsonSerializerOptions();
        }
        else
        {
            if(options.Value.SurrealIdOptions.HasFlag(SurrealIdOptions.Optimise))
            {
                jsonOptions = new JsonSerializerOptions
                {
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver
                    {
                        Modifiers = { InterceptId.Intercept }
                    }
                };
            }
            else
            {
                jsonOptions = new JsonSerializerOptions
                {
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver
                    {
                        Modifiers = { InterceptId.InterceptOptimised }
                    }
                };
            }
        }


    }

    public Uri Uri { get; }
    

    public void Dispose()
    {
    }
    
    public async Task<SurrealHttpResponse<bool>> Status(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/status");

        HttpResponseMessage response;

        response = await LoggedSend(request, cancellationToken);
        
        return responseProcessor.ProcessBoolResponse(response);
    }

    public async Task<SurrealHttpResponse<bool>> Health(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/health");

        using var response = await LoggedSend(request, cancellationToken);

        return responseProcessor.ProcessBoolResponse(response);
    }

    public async Task<SurrealHttpResponse<string>> Version(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/version");

        using var response = await LoggedSend(request, cancellationToken);
        
        if(!response.IsSuccessStatusCode)
            return SurrealHttpResponse<string>.Failure("Failed to retrieve version", response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        return SurrealHttpResponse<string>.Success(content, response.StatusCode);
    }

    public async Task<SurrealHttpArrayResponse<T>> Get<T>(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/key/{TableNameCache<T>.Name}");

        using var response = await LoggedSend(request, cancellationToken);

        return await responseProcessor.ProcessArrayResponse<T>(response, jsonOptions, cancellationToken);
    }

    public async Task<SurrealHttpResponse<T>> Get<T>(string id, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/key/{TableNameCache<T>.Name}/{id}");

        using var response = await LoggedSend(request, cancellationToken);

        return await responseProcessor.ProcessResponse<T>(response, jsonOptions, cancellationToken);
    }

    public async Task<SurrealHttpResponse<T>> Add<T>(T record, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"/key/{TableNameCache<T>.Name}");
        var body = JsonSerializer.Serialize(record);
        
        request.Content = new StringContent(body, Encoding.UTF8);

        using var response = await LoggedSend(request, cancellationToken);
        return await responseProcessor.ProcessResponse<T>(response, jsonOptions, cancellationToken);
    }

    public async Task<SurrealHttpResponse<bool>> Delete<T>(string id, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"/key/{TableNameCache<T>.Name}/{id}");

        using var response = await LoggedSend(request, cancellationToken);
        
        return responseProcessor.ProcessBoolResponse(response, "Failed to delete record.");
    }

    public async Task<SurrealHttpResponse<T>> Update<T>(T record, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/key/{TableNameCache<T>.Name}");
        var body = JsonSerializer.Serialize(record);
        
        request.Content = new StringContent(body, Encoding.UTF8);
        
        using var response = await LoggedSend(request, cancellationToken);
        return await responseProcessor.ProcessResponse<T>(response, jsonOptions,cancellationToken);
    }

    public async Task<SurrealHttpArrayResponse<T>> Query<T>(string query, IEnumerable<KeyValuePair<string, string?>>? parameters = null, CancellationToken cancellationToken = default)
    {
        var url = parameters != null ? QueryHelpers.AddQueryString($"/sql", parameters) : $"/sql";
        
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        using var response = await LoggedSend(request, cancellationToken);

        return await responseProcessor.ProcessArrayResponse<T>(response, jsonOptions, cancellationToken);
    }

    private async Task<HttpResponseMessage> LoggedSend(HttpRequestMessage request, CancellationToken cancelToken)
    {
        try
        {
            return await client.SendAsync(request, cancelToken);
        }
        catch (Exception ex)
        {
            LogCallError(ex, request.RequestUri.ToString());
            throw;
        }
    }
}
