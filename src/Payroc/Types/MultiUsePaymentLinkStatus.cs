using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(MultiUsePaymentLinkStatus.MultiUsePaymentLinkStatusSerializer))]
[Serializable]
public readonly record struct MultiUsePaymentLinkStatus : IStringEnum
{
    public static readonly MultiUsePaymentLinkStatus Active = new(Values.Active);

    public static readonly MultiUsePaymentLinkStatus Completed = new(Values.Completed);

    public static readonly MultiUsePaymentLinkStatus Deactivated = new(Values.Deactivated);

    public static readonly MultiUsePaymentLinkStatus Expired = new(Values.Expired);

    public MultiUsePaymentLinkStatus(string value)
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
    public static MultiUsePaymentLinkStatus FromCustom(string value)
    {
        return new MultiUsePaymentLinkStatus(value);
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

    public static bool operator ==(MultiUsePaymentLinkStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MultiUsePaymentLinkStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MultiUsePaymentLinkStatus value) => value.Value;

    public static explicit operator MultiUsePaymentLinkStatus(string value) => new(value);

    internal class MultiUsePaymentLinkStatusSerializer : JsonConverter<MultiUsePaymentLinkStatus>
    {
        public override MultiUsePaymentLinkStatus Read(
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
            return new MultiUsePaymentLinkStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MultiUsePaymentLinkStatus ReadAsPropertyName(
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
            return new MultiUsePaymentLinkStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkStatus value,
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
