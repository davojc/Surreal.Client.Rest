namespace Surreal.Client.Rest;

/// <summary>
/// This retrieves a token for use in the Api Calls. Default behaviour uses the 'SignIn' endpoint to get a token.
/// </summary>
public interface IIdentityTokenProvider
{
    Task<string?> GetToken(CancellationToken cancellationToken = default);
}
