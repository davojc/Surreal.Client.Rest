using Microsoft.Extensions.DependencyInjection;

namespace Surreal.Client.Rest.Test.Unit;

public class OptionsTestFixture : IDisposable
{
    public SurrealRestOptions Options { get; set; }

    public OptionsTestFixture() 
    {
        Options = SurrealRestOptions.Create()
            .WithEndpoint("http://127.0.0.1:8001")
            .WithUsername("root")
            .WithPassword("root")
            .WithDatabase("test")
            .WithDatabase("test").Build();
    }

    public void Dispose()
    {
        
    }

    public ServiceCollection GetServiceCollection()
    {
        var services = new ServiceCollection();
        services.AddSurrealRestClient(options =>
        {
            options.Database = "test";
            options.Namespace = "test";
            options.Password = "root";
            options.Username = "root";
            options.Endpoint = "http://test.com";
        });

        return services;
    }
}
