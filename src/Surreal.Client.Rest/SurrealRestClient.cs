using System.Text;
using System.Text.Json;
using Surreal.Client.Rest.Model;

namespace Surreal.Client.Rest;

public class SurrealRestClient(SurrealRestOptions options, HttpClient client) : ISurrealRestClient
{
    private string _token = string.Empty;
    
    public Uri Uri { get; } = new Uri(options.Endpoint);
    
    public void Dispose()
    {
    }
    
    public async Task<bool> Status(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/status");
        
        using var response = await client.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> Health(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/health");
        
        using var response = await client.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }

        return false;
    }

    public Task<string> Version(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SignIn(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/signin");
        
        var signin = options.CreateSignIn();
        var body= JsonSerializer.Serialize(signin);
        request.Content = new StringContent(body, Encoding.UTF8);
        
        using var response = await client.SendAsync(request, cancellationToken);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var authentication = JsonSerializer.Deserialize<Authentication>(content);

        if (authentication == null)
            return false;
        
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authentication.Token}");
        client.DefaultRequestHeaders.Add(HeaderNames.Namespace, options.Namespace);
        client.DefaultRequestHeaders.Add(HeaderNames.Database, options.Database);

        return true;
    }

    public async Task<IEnumerable<Response<T>>> Get<T>(string table, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/key/{table}");
        
        using var response = await client.SendAsync(request, cancellationToken);
        
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<Response<T>[]>(content);
    }

    public Task<T> Get<T>(string table, string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T> Add<T>(string table, T record, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string table, string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string table, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update<T>(string table, T record, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T> Query<T>(string query, IEnumerable<KeyValuePair<string, string>> parameters, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
