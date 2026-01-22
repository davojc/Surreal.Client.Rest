using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Test.Integration;

public class Parent
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var options = new SurrealRestOptionsBuilder()
            .WithEndpoint("http://127.0.0.1:8001")
            .WithUsername("root")
            .WithPassword("root")
            .WithDatabase("test")
            .WithDatabase("test").Build();
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(options.Endpoint);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var client = new SurrealRestClient(options, httpClient);

        var signin = await client.Health();
        
        Assert.True(signin);
    }
    
    [Fact]
    public async Task SignIn()
    {
        var options = new SurrealRestOptionsBuilder()
            .WithEndpoint("http://127.0.0.1:8001")
            .WithUsername("root")
            .WithPassword("root")
            .WithDatabase("test")
            .WithNamespace("test").Build();
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(options.Endpoint);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var client = new SurrealRestClient(options, httpClient);

        var signin = await client.SignIn();
        
        Assert.True(signin);
    }
    
    [Fact]
    public async Task GetAll()
    {
        var options = new SurrealRestOptionsBuilder()
            .WithEndpoint("http://127.0.0.1:8001")
            .WithUsername("root")
            .WithPassword("root")
            .WithDatabase("test")
            .WithNamespace("test").Build();
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(options.Endpoint);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var client = new SurrealRestClient(options, httpClient);

        await client.SignIn();
        var results = await client.Get<Parent>("parent");
        
        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }
}