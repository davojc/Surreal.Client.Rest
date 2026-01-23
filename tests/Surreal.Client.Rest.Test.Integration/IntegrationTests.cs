using Surreal.Client.Rest.Test.Integration.TestModel;

namespace Surreal.Client.Rest.Test.Integration;

public class IntegrationTests : IClassFixture<SurrealRestFixture>
{
    private readonly SurrealRestFixture _fixture;

    public IntegrationTests(SurrealRestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Test1()
    {
        var signin = await _fixture.Client.Health();
        
        Assert.True(signin.IsSuccess);
    }
        
    [Fact]
    public async Task GetAll()
    {
        var result = await _fixture.Client.Get<Parent>("parent");
        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddParent()
    {
        var parent = new Parent
        {
            Name = "Parent"
        };

        var result = await _fixture.Client.Add("parent", parent);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task AddChild()
    {
        var parent = await _fixture.Client.Get<Parent>("parent", "Parent");

        var child = new WriteChild
        {
            Name = "Child2",
            Parent = parent.Data.Id
        };

        var result = await _fixture.Client.Add("child", child);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }
}
