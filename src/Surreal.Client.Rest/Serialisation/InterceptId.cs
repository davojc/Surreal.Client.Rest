using System.Reflection;
using System.Text.Json.Serialization.Metadata;

namespace Surreal.Client.Rest.Serialisation;

internal static class InterceptId
{
    public static void Intercept(JsonTypeInfo typeInfo)
    {
        var tableAttr = typeInfo.Type.GetCustomAttribute<TableAttribute>();

        if (tableAttr == null) return;

        var idProp = typeInfo.Properties
            .FirstOrDefault(p => p.Name.Equals("id", StringComparison.OrdinalIgnoreCase));

        if (idProp != null && idProp.PropertyType == typeof(string))
        {
            // Pass the table name from the attribute to the converter
            idProp.CustomConverter = new SurrealIdConverter(tableAttr.Name);
        }
    }
}
