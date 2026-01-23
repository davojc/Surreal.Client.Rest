using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json;

namespace Surreal.Client.Rest.Test.Unit;

public class TestSurrealRestClient : IClassFixture<OptionsTestFixture>
{
    private readonly OptionsTestFixture fixture;

    public TestSurrealRestClient(OptionsTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task VerifyThatHealthWorks()
    {
        var signinResponse = new
        {
            code = 1,
            details = "Successful login",
            token = JwtHelper.GenerateToken()
        };

        var mockHttp = new MockHttpMessageHandler(BackendDefinitionBehavior.Always);
        mockHttp.When(HttpMethod.Post, "http://test.com/signin")
                .Respond("application/json", JsonSerializer.Serialize(signinResponse));

        mockHttp.Expect(HttpMethod.Get, "http://test.com/health")
            .WithHeaders("Accept", "application/json")
            .WithHeaders("Authorization", $"Bearer {signinResponse.token}")
            .Respond(HttpStatusCode.OK);

        var serviceCollection = fixture.GetServiceCollection();

        serviceCollection.ConfigureAll<HttpClientFactoryOptions>(options =>
        {
            options.HttpMessageHandlerBuilderActions.Add(builder =>
            {
                builder.PrimaryHandler = mockHttp;
            });
        });

        var provider = serviceCollection.BuildServiceProvider();

        var client = provider.GetRequiredService<ISurrealRestClient>();

        var result = await client.Health();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        mockHttp.VerifyNoOutstandingExpectation();
    }
}
