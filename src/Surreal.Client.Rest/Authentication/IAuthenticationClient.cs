namespace Surreal.Client.Rest.Authentication;

internal interface IAuthenticationClient
{
    Task<string?> FetchToken(CancellationToken cancellationToken = default);
}
