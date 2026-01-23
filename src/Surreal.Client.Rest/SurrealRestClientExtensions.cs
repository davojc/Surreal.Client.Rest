using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Surreal.Client.Rest;

public static class SurrealRestClientExtensions
{
    public static IServiceCollection AddSurrealRestClient(this IServiceCollection services, Action<SurrealRestOptions> options, IConfiguration? configuration = null)
    {
        if (configuration != null)
        {
            services.Configure<SurrealRestOptions>(configuration.GetSection("SurrealRest"));
        }
        else
        {
            services.Configure(options);
        }

        services.AddHttpClient<IIdentityClient, IdentityClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SurrealRestOptions>>().Value;

            client.BaseAddress = new Uri(options.Endpoint);
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        services.AddTransient<AuthHeaderHandler>();
        services.AddTransient<IIdentityTokenProvider, IdentityTokenProvider>();

        services.AddHttpClient<ISurrealRestClient, SurrealRestClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SurrealRestOptions>>().Value;

            client.BaseAddress = new Uri(options.Endpoint);
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            client.DefaultRequestHeaders.Add(HeaderNames.Namespace, options.Namespace);
            client.DefaultRequestHeaders.Add(HeaderNames.Database, options.Database);
        }).AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }
}
