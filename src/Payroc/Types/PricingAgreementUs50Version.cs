using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PricingAgreementUs50Version.PricingAgreementUs50VersionSerializer))]
[Serializable]
public readonly record struct PricingAgreementUs50Version : IStringEnum
{
    public static readonly PricingAgreementUs50Version Five0 = new(Values.Five0);

    public PricingAgreementUs50Version(string value)
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
    public static PricingAgreementUs50Version FromCustom(string value)
    {
        return new PricingAgreementUs50Version(value);
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

    public static bool operator ==(PricingAgreementUs50Version value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs50Version value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs50Version value) => value.Value;

    public static explicit operator PricingAgreementUs50Version(string value) => new(value);

    internal class PricingAgreementUs50VersionSerializer
        : JsonConverter<PricingAgreementUs50Version>
    {
        public override PricingAgreementUs50Version Read(
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
            return new PricingAgreementUs50Version(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PricingAgreementUs50Version value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PricingAgreementUs50Version ReadAsPropertyName(
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
            return new PricingAgreementUs50Version(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PricingAgreementUs50Version value,
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
        public const string Five0 = "5.0";
    }
}
