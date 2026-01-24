using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Surreal.Client.Rest.Metadata;
using Surreal.Client.Rest.Authentication;
using System.Reflection;
using Surreal.Client.Rest.Serialisation;

namespace Surreal.Client.Rest;

internal static class TableNameCache<T>
{
    public static readonly string Name;

    static TableNameCache()
    {
        var type = typeof(T);
        var tableAttr = type.GetCustomAttribute<TableAttribute>();

        if (tableAttr == null)
            throw new ArgumentException($"Cannot find the table name for the provided model: {type.Name}");

        Name = tableAttr.Name;
    }
}

public static class SurrealRestClientExtensions
{
    internal static string GetTableName(this Type modelType)
    {
        var tableAttr = modelType.GetCustomAttribute<TableAttribute>();

        if (tableAttr == null)
            throw new ArgumentException($"Cannot find the table name for the provided model: {modelType.Name}");

        return tableAttr.Name;
    }

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

        services.AddHttpClient<IAuthenticationClient, AuthenticationClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SurrealRestOptions>>().Value;

            client.BaseAddress = new Uri(options.Endpoint);
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        services.AddTransient<AuthHeaderHandler>();
        services.AddTransient<IIdentityTokenProvider, IdentityTokenProvider>();
        services.AddTransient<IResponseProcessor, ResponseProcessor>();

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
