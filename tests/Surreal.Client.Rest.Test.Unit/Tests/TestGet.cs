using RichardSzalay.MockHttp;
using Surreal.Client.Rest.Test.Unit.TestModel;
using System.ComponentModel;
using System.Net;
using System.Threading;

namespace Surreal.Client.Rest.Test.Unit.Tests;


public class TestGet : IClassFixture<OptionsTestFixture>
{
    private readonly OptionsTestFixture fixture;

    public TestGet(OptionsTestFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task Get_List_Of_Items()
    {
        var data = this.GetReturnData("Parents");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("/key/parent"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Get<Parent>();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Data.Length);

        mock.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Get_Single_Item()
    {
        var data = this.GetReturnData("Parents");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("/key/parent/ParentA"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Get<Parent>("ParentA");

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal("ParentA", result.Data.Name);
        Assert.Equal("ParentA", result.Data.Id);

        mock.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Get_Single_Item_With_SurrealIds()
    {
        var data = this.GetReturnData("Parents");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("/key/parent/ParentA"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler, true);

        var result = await client.Get<Parent>("ParentA");

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal("ParentA", result.Data.Name);
        Assert.Equal("parent:ParentA", result.Data.Id);

        mock.handler.VerifyNoOutstandingExpectation();
    }

    [Fact]
    public async Task Get_Items_With_Related_Record()
    {
        var data = this.GetReturnData("Children");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("/key/child/ChildA"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler);

        var result = await client.Get<WriteChild>("ChildA");

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal("ChildA", result.Data.Name);
        Assert.Equal("ChildA", result.Data.Id);
        Assert.Equal("ParentA", result.Data.Parent);

        mock.handler.VerifyNoOutstandingExpectation();
    }


    [Fact]
    public async Task Get_Items_With_Related_Record_With_SurrealIds()
    {
        var data = this.GetReturnData("Children");

        var mock = fixture.GetMockHandler();

        mock.handler.Expect(HttpMethod.Get, fixture.CreateMockPath("/key/child/ChildA"))
            .AcceptJson()
            .WithAuthorisation(mock.token)
            .Respond(HttpStatusCode.OK, "application/json", data);

        var client = fixture.GetClient(mock.handler, true);

        var result = await client.Get<WriteChild>("ChildA");

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.IsSuccess);
        Assert.Equal("ChildA", result.Data.Name);
        Assert.Equal("child:ChildA", result.Data.Id);
        Assert.Equal("parent:ParentA", result.Data.Parent);

        mock.handler.VerifyNoOutstandingExpectation();
    }
}
