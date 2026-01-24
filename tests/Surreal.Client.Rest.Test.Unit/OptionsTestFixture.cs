using System.Net;
using System.Security.Principal;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using RichardSzalay.MockHttp;

namespace Surreal.Client.Rest.Test.Unit;

public class OptionsTestFixture : IDisposable
{
    private string endPoint = "http://test.com";

    public void Dispose()
    {
        
    }

    public string CreateMockPath(string suffix)
    {
        var trimmed = suffix.TrimStart('/');

        return $"{endPoint}/{trimmed}";
    }

    public (MockHttpMessageHandler handler, string token) GetMockHandler()
    {
        var signinResponse = new
        {
            code = 1,
            details = "Successful login",
            token = JwtHelper.GenerateToken()
        };

        var mockHttp = new MockHttpMessageHandler(BackendDefinitionBehavior.Always);
        mockHttp.When(HttpMethod.Post, CreateMockPath("signin"))
                .Respond("application/json", JsonSerializer.Serialize(signinResponse));

        return (mockHttp, signinResponse.token);
    }

    public ISurrealRestClient GetClient(MockHttpMessageHandler handler, bool exposeSurrealId = false)
    {
        var services = new ServiceCollection();
        services.AddSurrealRestClient(options =>
        {
            options.Database = "test";
            options.Namespace = "test";
            options.Password = "root";
            options.Username = "root";
            options.Endpoint = endPoint;
            options.SurrealIdOptions = exposeSurrealId ? SurrealIdOptions.ExposeSurrealIds : SurrealIdOptions.None;
        });

        services.ConfigureAll<HttpClientFactoryOptions>(options =>
        {
            options.HttpMessageHandlerBuilderActions.Add(builder =>
            {
                builder.PrimaryHandler = handler;
            });
        });

        var provider = services.BuildServiceProvider();

        return provider.GetRequiredService<ISurrealRestClient>();
    }
}
