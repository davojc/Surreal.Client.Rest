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
}