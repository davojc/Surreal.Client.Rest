using System.IdentityModel.Tokens.Jwt;

namespace Surreal.Client.Rest.Authentication;

internal class IdentityTokenProvider(IAuthenticationClient client) : IIdentityTokenProvider
{
    private JwtSecurityToken? _token = null;
    private string? _tokenString = null;
    private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

    public async Task<string?> GetToken(CancellationToken cancellationToken = default)
    {
        if(_token == null || _token.ValidTo <= DateTime.UtcNow)
        {
            _tokenString = await client.FetchToken();
            _token = _tokenHandler.ReadJwtToken(_tokenString);
        }

        return _tokenString;
    }
}
