using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ContactType.ContactTypeSerializer))]
[Serializable]
public readonly record struct ContactType : IStringEnum
{
    public static readonly ContactType Manager = new(Values.Manager);

    public static readonly ContactType Representative = new(Values.Representative);

    public static readonly ContactType Others = new(Values.Others);

    public ContactType(string value)
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
    public static ContactType FromCustom(string value)
    {
        return new ContactType(value);
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

    public static bool operator ==(ContactType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ContactType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ContactType value) => value.Value;

    public static explicit operator ContactType(string value) => new(value);

    internal class ContactTypeSerializer : JsonConverter<ContactType>
    {
        public override ContactType Read(
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
            return new ContactType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ContactType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ContactType ReadAsPropertyName(
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
            return new ContactType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ContactType value,
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
        public const string Manager = "manager";

        public const string Representative = "representative";

        public const string Others = "others";
    }
}
