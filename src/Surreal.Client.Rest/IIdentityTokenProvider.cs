namespace Surreal.Client.Rest;

public interface IIdentityTokenProvider
{
    Task<string?> GetToken(CancellationToken cancellationToken = default);
}
