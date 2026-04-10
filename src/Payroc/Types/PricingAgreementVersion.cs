using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementVersion.PricingAgreementVersionSerializer))]
[Serializable]
public readonly record struct PricingAgreementVersion : IStringEnum
{
    public static readonly PricingAgreementVersion Five2 = new(Values.Five2);

    public PricingAgreementVersion(string value)
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
    public static PricingAgreementVersion FromCustom(string value)
    {
        return new PricingAgreementVersion(value);
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

    public static bool operator ==(PricingAgreementVersion value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementVersion value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementVersion value) => value.Value;

    public static explicit operator PricingAgreementVersion(string value) => new(value);

    internal class PricingAgreementVersionSerializer : JsonConverter<PricingAgreementVersion>
    {
        public override PricingAgreementVersion Read(
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
            return new PricingAgreementVersion(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementVersion value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PricingAgreementVersion ReadAsPropertyName(
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
            return new PricingAgreementVersion(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PricingAgreementVersion value,
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
