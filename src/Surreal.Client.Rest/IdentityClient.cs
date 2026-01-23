using Microsoft.Extensions.Options;
using Surreal.Client.Rest.Model;
using System.Text;
using System.Text.Json;

namespace Surreal.Client.Rest;

internal class IdentityClient(HttpClient client, IOptions<SurrealRestOptions> options) : IIdentityClient
{
    

    public async Task<string?> FetchToken(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "/signin");

        var signin = options.Value.CreateSignIn();
        var body = JsonSerializer.Serialize(signin);
        request.Content = new StringContent(body, Encoding.UTF8);

        using var response = await client.SendAsync(request, cancellationToken);

        var content = await response.Content.ReadAsStringAsync();

        var authentication = JsonSerializer.Deserialize<Authentication>(content);

        if (authentication == null)
        {
            return null;
        }

        return authentication.Token;
    }
}
