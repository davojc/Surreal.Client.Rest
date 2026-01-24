using Surreal.Client.Rest.Metadata;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;

namespace Surreal.Client.Rest.Serialisation;

internal static class InterceptId
{
    public static void Intercept(JsonTypeInfo typeInfo)
    {
        InterceptCommon(typeInfo, false);
    }

    public static void InterceptOptimised(JsonTypeInfo typeInfo)
    {
        InterceptCommon(typeInfo, true);
    }

    private static void InterceptCommon(JsonTypeInfo typeInfo, bool optimise)
    {
        var tableAttr = typeInfo.Type.GetCustomAttribute<TableAttribute>();

        if (tableAttr == null) return;

        foreach(var property in typeInfo.Properties)
        {
            if (property.PropertyType != typeof(string))
                continue;

            if(string.Equals(property.Name, "id", StringComparison.OrdinalIgnoreCase))
            {
                property.CustomConverter = new SurrealIdConverter(tableAttr.Name, optimise);
                continue;
            }

            if (property.AttributeProvider?.IsDefined(typeof(IdFieldAttribute), inherit: false) != true)
                continue;


            var fieldAttribute = property.AttributeProvider
                                   .GetCustomAttributes(typeof(IdFieldAttribute), false)
                                   .FirstOrDefault() as IdFieldAttribute;

            if (fieldAttribute == null)
                continue;

            var tableName = fieldAttribute.Parent.GetTableName();

            property.CustomConverter = new SurrealIdConverter(tableName, optimise);
        }
    }
}
