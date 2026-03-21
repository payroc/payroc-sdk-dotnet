using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(MultiUsePaymentLinkAuthType.MultiUsePaymentLinkAuthTypeSerializer))]
[Serializable]
public readonly record struct MultiUsePaymentLinkAuthType : IStringEnum
{
    public static readonly MultiUsePaymentLinkAuthType Sale = new(Values.Sale);

    public static readonly MultiUsePaymentLinkAuthType PreAuthorization = new(
        Values.PreAuthorization
    );

    public MultiUsePaymentLinkAuthType(string value)
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
    public static MultiUsePaymentLinkAuthType FromCustom(string value)
    {
        return new MultiUsePaymentLinkAuthType(value);
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

    public static bool operator ==(MultiUsePaymentLinkAuthType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MultiUsePaymentLinkAuthType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MultiUsePaymentLinkAuthType value) => value.Value;

    public static explicit operator MultiUsePaymentLinkAuthType(string value) => new(value);

    internal class MultiUsePaymentLinkAuthTypeSerializer
        : JsonConverter<MultiUsePaymentLinkAuthType>
    {
        public override MultiUsePaymentLinkAuthType Read(
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
            return new MultiUsePaymentLinkAuthType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkAuthType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MultiUsePaymentLinkAuthType ReadAsPropertyName(
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
            return new MultiUsePaymentLinkAuthType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkAuthType value,
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
        public const string Sale = "sale";

        public const string PreAuthorization = "preAuthorization";
    }
}
