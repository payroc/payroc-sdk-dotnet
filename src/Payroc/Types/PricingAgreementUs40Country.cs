using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementUs40Country.PricingAgreementUs40CountrySerializer))]
[Serializable]
public readonly record struct PricingAgreementUs40Country : IStringEnum
{
    public static readonly PricingAgreementUs40Country Us = new(Values.Us);

    public PricingAgreementUs40Country(string value)
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
    public static PricingAgreementUs40Country FromCustom(string value)
    {
        return new PricingAgreementUs40Country(value);
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

    public static bool operator ==(PricingAgreementUs40Country value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs40Country value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs40Country value) => value.Value;

    public static explicit operator PricingAgreementUs40Country(string value) => new(value);

    internal class PricingAgreementUs40CountrySerializer
        : JsonConverter<PricingAgreementUs40Country>
    {
        public override PricingAgreementUs40Country Read(
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
            return new PricingAgreementUs40Country(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs40Country value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PricingAgreementUs40Country ReadAsPropertyName(
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
            return new PricingAgreementUs40Country(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PricingAgreementUs40Country value,
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
