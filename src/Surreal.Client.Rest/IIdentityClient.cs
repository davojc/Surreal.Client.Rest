namespace Surreal.Client.Rest;

internal interface IIdentityClient
{
    Task<string?> FetchToken(CancellationToken cancellationToken = default);
}
