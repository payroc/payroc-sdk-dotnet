using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(CustomerNotificationLanguage.CustomerNotificationLanguageSerializer))]
[Serializable]
public readonly record struct CustomerNotificationLanguage : IStringEnum
{
    public static readonly CustomerNotificationLanguage En = new(Values.En);

    public static readonly CustomerNotificationLanguage Fr = new(Values.Fr);

    public CustomerNotificationLanguage(string value)
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
    public static CustomerNotificationLanguage FromCustom(string value)
    {
        return new CustomerNotificationLanguage(value);
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

    public static bool operator ==(CustomerNotificationLanguage value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CustomerNotificationLanguage value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CustomerNotificationLanguage value) => value.Value;

    public static explicit operator CustomerNotificationLanguage(string value) => new(value);

    internal class CustomerNotificationLanguageSerializer
        : JsonConverter<CustomerNotificationLanguage>
    {
        public override CustomerNotificationLanguage Read(
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
            return new CustomerNotificationLanguage(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CustomerNotificationLanguage value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CustomerNotificationLanguage ReadAsPropertyName(
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
            return new CustomerNotificationLanguage(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CustomerNotificationLanguage value,
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
        public const string En = "en";

        public const string Fr = "fr";
    }
}
