using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SubscriptionFrequency.SubscriptionFrequencySerializer))]
[Serializable]
public readonly record struct SubscriptionFrequency : IStringEnum
{
    public static readonly SubscriptionFrequency Weekly = new(Values.Weekly);

    public static readonly SubscriptionFrequency Fortnightly = new(Values.Fortnightly);

    public static readonly SubscriptionFrequency Monthly = new(Values.Monthly);

    public static readonly SubscriptionFrequency Quarterly = new(Values.Quarterly);

    public static readonly SubscriptionFrequency Yearly = new(Values.Yearly);

    public SubscriptionFrequency(string value)
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
    public static SubscriptionFrequency FromCustom(string value)
    {
        return new SubscriptionFrequency(value);
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

    public static bool operator ==(SubscriptionFrequency value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionFrequency value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionFrequency value) => value.Value;

    public static explicit operator SubscriptionFrequency(string value) => new(value);

    internal class SubscriptionFrequencySerializer : JsonConverter<SubscriptionFrequency>
    {
        public override SubscriptionFrequency Read(
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
            return new SubscriptionFrequency(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SubscriptionFrequency value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SubscriptionFrequency ReadAsPropertyName(
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
            return new SubscriptionFrequency(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SubscriptionFrequency value,
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
        public const string Weekly = "weekly";

        public const string Fortnightly = "fortnightly";

        public const string Monthly = "monthly";

        public const string Quarterly = "quarterly";

        public const string Yearly = "yearly";
    }
}
