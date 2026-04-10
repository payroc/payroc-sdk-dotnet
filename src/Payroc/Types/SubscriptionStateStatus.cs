using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SubscriptionStateStatus.SubscriptionStateStatusSerializer))]
[Serializable]
public readonly record struct SubscriptionStateStatus : IStringEnum
{
    public static readonly SubscriptionStateStatus Active = new(Values.Active);

    public static readonly SubscriptionStateStatus Completed = new(Values.Completed);

    public static readonly SubscriptionStateStatus Suspended = new(Values.Suspended);

    public static readonly SubscriptionStateStatus Cancelled = new(Values.Cancelled);

    public SubscriptionStateStatus(string value)
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
    public static SubscriptionStateStatus FromCustom(string value)
    {
        return new SubscriptionStateStatus(value);
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

    public static bool operator ==(SubscriptionStateStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionStateStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionStateStatus value) => value.Value;

    public static explicit operator SubscriptionStateStatus(string value) => new(value);

    internal class SubscriptionStateStatusSerializer : JsonConverter<SubscriptionStateStatus>
    {
        public override SubscriptionStateStatus Read(
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
            return new SubscriptionStateStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SubscriptionStateStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SubscriptionStateStatus ReadAsPropertyName(
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
            return new SubscriptionStateStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SubscriptionStateStatus value,
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
        public const string Active = "active";

        public const string Completed = "completed";

        public const string Suspended = "suspended";

        public const string Cancelled = "cancelled";
    }
}
