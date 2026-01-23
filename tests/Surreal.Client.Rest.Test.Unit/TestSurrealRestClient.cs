using RichardSzalay.MockHttp;
using System.Net;

namespace Surreal.Client.Rest.Test.Unit;

public class TestSurrealRestClient : IClassFixture<OptionsTestFixture>
{
    private readonly OptionsTestFixture fixture;

    public TestSurrealRestClient(OptionsTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task VerifyHealthEndpoint()
    {
        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("health"))
            .WithHeaders("Accept", "application/json")
            .WithHeaders("Authorization", $"Bearer {mock.token}")
            .Respond(HttpStatusCode.OK);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Health();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        mock.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task VerifyStatusEndpoint()
    {
        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("status"))
            .WithHeaders("Accept", "application/json")
            .WithHeaders("Authorization", $"Bearer {mock.token}")
            .Respond(HttpStatusCode.OK);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Status();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        mock.handler.VerifyNoOutstandingExpectation();
    }
}
