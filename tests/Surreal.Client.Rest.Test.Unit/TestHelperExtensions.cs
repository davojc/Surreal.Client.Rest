using RichardSzalay.MockHttp;
using System.Runtime.CompilerServices;

namespace Surreal.Client.Rest.Test.Unit;

public static class TestHelperExtensions
{
    public static MockedRequest AcceptJson(this MockedRequest request)
    {
        return request.WithHeaders("Accept", "application/json");
    }

    public static MockedRequest WithAuthorisation(this MockedRequest request, string token)
    {
        return request.WithHeaders("Authorization", $"Bearer {token}");
    }

    public static string GetReturnData(this object testClass, string? name = null, [CallerMemberName] string methodName = "")
    {
        var type = testClass.GetType();
        var assembly = type.Assembly;

        var namespacePath = type.Assembly.GetName().Name;

        var resource = !string.IsNullOrWhiteSpace(name) ? name : methodName;

        var resourceName = $"{namespacePath}.Data.{type.Name}.{resource}.json";

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == null)
        {
            var available = string.Join("\n", assembly.GetManifestResourceNames());
            throw new FileNotFoundException(
                $"Could not find embedded JSON at '{resourceName}'.\n" +
                $"Ensure the file name matches the test method: '{methodName}.json'\n" +
                $"And is in the same folder as the test class.\n\n" +
                $"Available Resources:\n{available}");
        }

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
