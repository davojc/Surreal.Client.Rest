using RichardSzalay.MockHttp;
using Surreal.Client.Rest.Test.Unit.TestModel;
using System.ComponentModel;
using System.Net;
using System.Threading;

namespace Surreal.Client.Rest.Test.Unit.Tests;


public class TestDelete : IClassFixture<OptionsTestFixture>
{
    private readonly OptionsTestFixture fixture;

    public TestDelete(OptionsTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task Delete_Individual_Item()
    {
        var data = this.GetReturnData("Parent");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Post, fixture.CreateMockPath("/key/parent/ParentA"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Delete<Parent>("ParentA");

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);

        mock.handler.VerifyNoOutstandingExpectation();
    }
}
