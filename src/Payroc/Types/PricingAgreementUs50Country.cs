using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementUs50Country.PricingAgreementUs50CountrySerializer))]
[Serializable]
public readonly record struct PricingAgreementUs50Country : IStringEnum
{
    public static readonly PricingAgreementUs50Country Us = new(Values.Us);

    public PricingAgreementUs50Country(string value)
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
    public static PricingAgreementUs50Country FromCustom(string value)
    {
        return new PricingAgreementUs50Country(value);
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

    public static bool operator ==(PricingAgreementUs50Country value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs50Country value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs50Country value) => value.Value;

    public static explicit operator PricingAgreementUs50Country(string value) => new(value);

    internal class PricingAgreementUs50CountrySerializer
        : JsonConverter<PricingAgreementUs50Country>
    {
        public override PricingAgreementUs50Country Read(
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
            return new PricingAgreementUs50Country(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs50Country value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PricingAgreementUs50Country ReadAsPropertyName(
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
            return new PricingAgreementUs50Country(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PricingAgreementUs50Country value,
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
        public const string Us = "US";
    }
}
