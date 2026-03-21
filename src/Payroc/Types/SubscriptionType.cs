using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SubscriptionType.SubscriptionTypeSerializer))]
[Serializable]
public readonly record struct SubscriptionType : IStringEnum
{
    public static readonly SubscriptionType Manual = new(Values.Manual);

    public static readonly SubscriptionType Automatic = new(Values.Automatic);

    public SubscriptionType(string value)
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
    public static SubscriptionType FromCustom(string value)
    {
        return new SubscriptionType(value);
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

    public static bool operator ==(SubscriptionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionType value) => value.Value;

    public static explicit operator SubscriptionType(string value) => new(value);

    internal class SubscriptionTypeSerializer : JsonConverter<SubscriptionType>
    {
        public override SubscriptionType Read(
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
            return new SubscriptionType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SubscriptionType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SubscriptionType ReadAsPropertyName(
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
            return new SubscriptionType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SubscriptionType value,
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
        public const string Manual = "manual";

        public const string Automatic = "automatic";
    }
}
