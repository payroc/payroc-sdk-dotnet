using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(AddressTypeType.AddressTypeTypeSerializer))]
[Serializable]
public readonly record struct AddressTypeType : IStringEnum
{
    public static readonly AddressTypeType LegalAddress = new(Values.LegalAddress);

    public AddressTypeType(string value)
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
    public static AddressTypeType FromCustom(string value)
    {
        return new AddressTypeType(value);
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

    public static bool operator ==(AddressTypeType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AddressTypeType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AddressTypeType value) => value.Value;

    public static explicit operator AddressTypeType(string value) => new(value);

    internal class AddressTypeTypeSerializer : JsonConverter<AddressTypeType>
    {
        public override AddressTypeType Read(
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
            return new AddressTypeType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AddressTypeType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AddressTypeType ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new AddressTypeType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AddressTypeType value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string LegalAddress = "legalAddress";
    }
}
