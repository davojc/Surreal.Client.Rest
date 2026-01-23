using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Surreal.Client.Rest.Test.Integration;

public class SurrealRestFixture : IDisposable
{
    public SurrealRestOptions Options { get; set; }
    public ISurrealRestClient Client { get; set; }

    public SurrealRestFixture()
    {
        Options = SurrealRestOptions.Create()
            .WithEndpoint("http://127.0.0.1:8001")
            .WithUsername("root")
            .WithPassword("root")
            .WithDatabase("test")
            .WithDatabase("test").Build();

        var services = new ServiceCollection();
        services.AddSurrealRestClient(options =>
        {
            options.Database = "test";
            options.Namespace = "test";
            options.Password = "root";
            options.Username = "root";
            options.Endpoint = "http://127.0.0.1:8001";
        });

        Client = services.BuildServiceProvider().GetService<ISurrealRestClient>();
    }

    public void Dispose()
    {

    }
}
