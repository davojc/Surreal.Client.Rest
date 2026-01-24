namespace Surreal.Client.Rest.Metadata;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class IdFieldAttribute(Type parentType) : Attribute
{
    public Type Parent { get; } = parentType;
}
