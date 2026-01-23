using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Surreal.Client.Rest.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace Surreal.Client.Rest;

public class SurrealRestClient(HttpClient client, IOptions<SurrealRestOptions> options) : ISurrealRestClient
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
    private JwtSecurityToken? _token = null;

    public Uri Uri { get; } = new Uri(options.Value.Endpoint);
    
    public void Dispose()
    {
    }
    
    public async Task<SurrealHttpResponse<bool>> Status(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/status");
        
        using var response = await client.SendAsync(request, cancellationToken);

        return response.ProcessBoolResponse();
    }

    public async Task<SurrealHttpResponse<bool>> Health(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/health");
        
        using var response = await client.SendAsync(request, cancellationToken);

        return response.ProcessBoolResponse();
    }

    public async Task<SurrealHttpResponse<string>> Version(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/version");
        
        using var response = await client.SendAsync(request, cancellationToken);
        
        if(!response.IsSuccessStatusCode)
            return SurrealHttpResponse<string>.Failure("Failed to retrieve version", response.StatusCode);
        
        var content = await response.Content.ReadAsStringAsync();
        return SurrealHttpResponse<string>.Success(content, response.StatusCode);
    }

    public async Task<SurrealHttpArrayResponse<T>> Get<T>(string table, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/key/{table}");
        
        using var response = await client.SendAsync(request, cancellationToken);

        return await response.ProcessArrayResponse<T>(cancellationToken);
    }

    public async Task<SurrealHttpResponse<T>> Get<T>(string table, string id, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/key/{table}");
        
        using var response = await client.SendAsync(request, cancellationToken);
        return await response.ProcessResponse<T>(cancellationToken);
    }

    public async Task<SurrealHttpResponse<T>> Add<T>(string table, T record, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"/key/{table}");
        var body = JsonSerializer.Serialize(record);
        
        request.Content = new StringContent(body, Encoding.UTF8);
        
        using var response = await client.SendAsync(request, cancellationToken);
        return await response.ProcessResponse<T>(cancellationToken);
    }

    public async Task<SurrealHttpResponse<bool>> Delete(string table, string id, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"/key/{table}/{id}");
        using var response = await client.SendAsync(request, cancellationToken);
        
        return response.ProcessBoolResponse("Failed to delete record.", cancellationToken);
    }

    public async Task<SurrealHttpResponse<bool>> Delete(string table, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"/key/{table}");
        using var response = await client.SendAsync(request, cancellationToken);
        
        return response.ProcessBoolResponse("Failed to delete record.", cancellationToken);
    }

    public async Task<SurrealHttpResponse<T>> Update<T>(string table, T record, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/key/{table}");
        var body = JsonSerializer.Serialize(record);
        
        request.Content = new StringContent(body, Encoding.UTF8);
        
        using var response = await client.SendAsync(request, cancellationToken);
        return await response.ProcessResponse<T>(cancellationToken);
    }

    public async Task<SurrealHttpArrayResponse<T>> Query<T>(string query, IEnumerable<KeyValuePair<string, string?>>? parameters = null, CancellationToken cancellationToken = default)
    {
        var url = parameters != null ? QueryHelpers.AddQueryString($"/sql", parameters) : $"/sql";
        
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        using var response = await client.SendAsync(request, cancellationToken);

        return await response.ProcessArrayResponse<T>(cancellationToken);
    }
}
