using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementUs52Version.PricingAgreementUs52VersionSerializer))]
[Serializable]
public readonly record struct PricingAgreementUs52Version : IStringEnum
{
    public static readonly PricingAgreementUs52Version Five2 = new(Values.Five2);

    public PricingAgreementUs52Version(string value)
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
    public static PricingAgreementUs52Version FromCustom(string value)
    {
        return new PricingAgreementUs52Version(value);
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

    public static bool operator ==(PricingAgreementUs52Version value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs52Version value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs52Version value) => value.Value;

    public static explicit operator PricingAgreementUs52Version(string value) => new(value);

    internal class PricingAgreementUs52VersionSerializer
        : JsonConverter<PricingAgreementUs52Version>
    {
        public override PricingAgreementUs52Version Read(
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
            return new PricingAgreementUs52Version(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs52Version value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PricingAgreementUs52Version ReadAsPropertyName(
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
            return new PricingAgreementUs52Version(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PricingAgreementUs52Version value,
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
        public const string Five2 = "5.2";
    }
}
