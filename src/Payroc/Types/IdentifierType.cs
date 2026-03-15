using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(IdentifierType.IdentifierTypeSerializer))]
[Serializable]
public readonly record struct IdentifierType : IStringEnum
{
    public static readonly IdentifierType NationalId = new(Values.NationalId);

    public IdentifierType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static IdentifierType FromCustom(string value)
    {
        return new IdentifierType(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(IdentifierType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(IdentifierType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(IdentifierType value) => value.Value;

    public static explicit operator IdentifierType(string value) => new(value);

    internal class IdentifierTypeSerializer : JsonConverter<IdentifierType>
    {
        public override IdentifierType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new IdentifierType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            IdentifierType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string NationalId = "nationalId";
    }
}
