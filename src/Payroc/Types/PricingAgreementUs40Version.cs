using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementUs40Version.PricingAgreementUs40VersionSerializer))]
[Serializable]
public readonly record struct PricingAgreementUs40Version : IStringEnum
{
    public static readonly PricingAgreementUs40Version Four0 = new(Values.Four0);

    public PricingAgreementUs40Version(string value)
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
    public static PricingAgreementUs40Version FromCustom(string value)
    {
        return new PricingAgreementUs40Version(value);
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

    public static bool operator ==(PricingAgreementUs40Version value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs40Version value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs40Version value) => value.Value;

    public static explicit operator PricingAgreementUs40Version(string value) => new(value);

    internal class PricingAgreementUs40VersionSerializer
        : JsonConverter<PricingAgreementUs40Version>
    {
        public override PricingAgreementUs40Version Read(
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
            return new PricingAgreementUs40Version(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs40Version value,
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
        public const string Four0 = "4.0";
    }
}
