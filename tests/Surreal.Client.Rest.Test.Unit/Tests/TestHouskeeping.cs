using RichardSzalay.MockHttp;
using Surreal.Client.Rest.Test.Unit.TestModel;
using System.ComponentModel;
using System.Net;
using System.Threading;

namespace Surreal.Client.Rest.Test.Unit.Tests;


public class TestHousekeeping : IClassFixture<OptionsTestFixture>
{
    private readonly OptionsTestFixture fixture;

    public TestHousekeeping(OptionsTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task VerifyHealthEndpoint()
    {
        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("health"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
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
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Status();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        mock.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task VersionStatusEndpoint()
    {
        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("version"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "text/plain", "surrealdb-2.5.0");

        var client = fixture.GetClient(mock.handler);

        var result = await client.Version();

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("surrealdb-2.5.0", result.Data);

        mock.handler.VerifyNoOutstandingExpectation();
    }
}
