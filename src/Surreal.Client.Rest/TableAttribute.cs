namespace Surreal.Client.Rest;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TableAttribute(string tableName) : Attribute
{
    public string Name { get; } = tableName;
}
