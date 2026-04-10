using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SingleUsePaymentLinkStatus.SingleUsePaymentLinkStatusSerializer))]
[Serializable]
public readonly record struct SingleUsePaymentLinkStatus : IStringEnum
{
    public static readonly SingleUsePaymentLinkStatus Active = new(Values.Active);

    public static readonly SingleUsePaymentLinkStatus Completed = new(Values.Completed);

    public static readonly SingleUsePaymentLinkStatus Deactivated = new(Values.Deactivated);

    public static readonly SingleUsePaymentLinkStatus Expired = new(Values.Expired);

    public SingleUsePaymentLinkStatus(string value)
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
    public static SingleUsePaymentLinkStatus FromCustom(string value)
    {
        return new SingleUsePaymentLinkStatus(value);
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

    public static bool operator ==(SingleUsePaymentLinkStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUsePaymentLinkStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SingleUsePaymentLinkStatus value) => value.Value;

    public static explicit operator SingleUsePaymentLinkStatus(string value) => new(value);

    internal class SingleUsePaymentLinkStatusSerializer : JsonConverter<SingleUsePaymentLinkStatus>
    {
        public override SingleUsePaymentLinkStatus Read(
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
            return new SingleUsePaymentLinkStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SingleUsePaymentLinkStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SingleUsePaymentLinkStatus ReadAsPropertyName(
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
            return new SingleUsePaymentLinkStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SingleUsePaymentLinkStatus value,
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

        public const string Deactivated = "deactivated";

        public const string Expired = "expired";
    }
}
