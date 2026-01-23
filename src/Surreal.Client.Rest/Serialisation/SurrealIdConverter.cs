using System.Text.Json;
using System.Text.Json.Serialization;

namespace Surreal.Client.Rest.Serialisation;

internal class SurrealIdConverter : JsonConverter<string>
{
    private readonly string _prefix;
    private readonly int _prefixLen;
    private readonly bool _optimise;

    public SurrealIdConverter(string tableName, bool optimise = true)
    {
        _prefix = tableName + ":";
        _prefixLen = _prefix.Length;
        this._optimise = optimise;
    }

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        string? value = reader.GetString();

        if (string.IsNullOrEmpty(value))
            return value;

        if (_optimise)
        {
            ReadOnlySpan<char> valSpan = value.AsSpan();
            ReadOnlySpan<char> prefixSpan = _prefix.AsSpan();

            if (valSpan.Length > _prefixLen &&
                valSpan.StartsWith(prefixSpan, StringComparison.OrdinalIgnoreCase))
            {
                return valSpan.Slice(_prefixLen).ToString();
            }

            return value;
        }
        else
        {
            if (value.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
            {
                return value.Substring(_prefix.Length);
            }

            return value;
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        if (string.IsNullOrEmpty(value))
        {
            writer.WriteStringValue(value);
            return;
        }

        if (_optimise)
        {
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
        else
        {
            if (value.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
            {
                writer.WriteStringValue(value);
            }
            else
            {
                writer.WriteStringValue($"{_prefix}{value}");
            }
        }
    }
}