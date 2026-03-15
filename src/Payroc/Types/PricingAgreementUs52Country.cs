using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementUs52Country.PricingAgreementUs52CountrySerializer))]
[Serializable]
public readonly record struct PricingAgreementUs52Country : IStringEnum
{
    public static readonly PricingAgreementUs52Country Us = new(Values.Us);

    public PricingAgreementUs52Country(string value)
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
    public static PricingAgreementUs52Country FromCustom(string value)
    {
        return new PricingAgreementUs52Country(value);
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

    public static bool operator ==(PricingAgreementUs52Country value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs52Country value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs52Country value) => value.Value;

    public static explicit operator PricingAgreementUs52Country(string value) => new(value);

    internal class PricingAgreementUs52CountrySerializer
        : JsonConverter<PricingAgreementUs52Country>
    {
        public override PricingAgreementUs52Country Read(
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
            return new PricingAgreementUs52Country(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs52Country value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
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
