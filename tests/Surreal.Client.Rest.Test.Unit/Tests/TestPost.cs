using RichardSzalay.MockHttp;
using Surreal.Client.Rest.Test.Unit.TestModel;
using System.Net;

namespace Surreal.Client.Rest.Test.Unit.Tests;


public class TestPost : IClassFixture<OptionsTestFixture>
{
    private readonly OptionsTestFixture fixture;

    public TestPost(OptionsTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task Post_New_Item()
    {
        var data = this.GetReturnData("Parent");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Post, fixture.CreateMockPath("/key/parent"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler);

        var parent = new Parent
        {
            Name = "ParentA"
        };

        var result = await client.Add(parent);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal("ParentA", result.Data.Id);
        Assert.Equal("ParentA", result.Data.Name);

        mock.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Post_New_Item_With_SurrealIds()
    {
        var data = this.GetReturnData("Parent");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Post, fixture.CreateMockPath("/key/parent"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler);

        var parent = new Parent
        {
            Name = "ParentA"
        };

        var result = await client.Add(parent);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal("ParentA", result.Data.Id);
        Assert.Equal("ParentA", result.Data.Name);

        mock.handler.VerifyNoOutstandingExpectation();
    }
}
