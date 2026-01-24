using System.Text.Json;
using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Serialisation;

internal class SurrealIdConverter : JsonConverter<string>
{
    private readonly string _prefix;
    private readonly int _prefixLen;

    public SurrealIdConverter(string tableName)
    {
        _prefix = tableName + ":";
        _prefixLen = _prefix.Length;
    }

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        string? value = reader.GetString();

        if (string.IsNullOrEmpty(value))
            return value;

        ReadOnlySpan<char> valSpan = value.AsSpan();
        ReadOnlySpan<char> prefixSpan = _prefix.AsSpan();

        if (valSpan.Length > _prefixLen &&
            valSpan.StartsWith(prefixSpan, StringComparison.OrdinalIgnoreCase))
        {
            return valSpan.Slice(_prefixLen).ToString();
        }

        return value;
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (string.IsNullOrEmpty(value))
        {
            writer.WriteStringValue(value);
            return;
        }

        ReadOnlySpan<char> valSpan = value.AsSpan();
        ReadOnlySpan<char> prefixSpan = _prefix.AsSpan();

        if (valSpan.Length >= _prefixLen &&
            valSpan.StartsWith(prefixSpan, StringComparison.OrdinalIgnoreCase))
        {
            writer.WriteStringValue(valSpan);
            return;
        }

        int requiredLength = _prefixLen + valSpan.Length;

        Span<char> buffer = requiredLength <= 256
            ? stackalloc char[requiredLength]
            : new char[requiredLength];

        prefixSpan.CopyTo(buffer);
        valSpan.CopyTo(buffer.Slice(_prefixLen));
        writer.WriteStringValue(buffer);
    }
}