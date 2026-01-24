using Surreal.Client.Rest.Test.Integration.TestModel;

namespace Surreal.Client.Rest.Test.Integration;

public class IntegrationTests : IClassFixture<SurrealRestFixture>
{
    private readonly SurrealRestFixture _fixture;

    public IntegrationTests(SurrealRestFixture fixture)
    {
        _fixture = fixture;
    }
}
