using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(DualPricingAlternativeTender.DualPricingAlternativeTenderSerializer))]
[Serializable]
public readonly record struct DualPricingAlternativeTender : IStringEnum
{
    public static readonly DualPricingAlternativeTender Card = new(Values.Card);

    public static readonly DualPricingAlternativeTender Cash = new(Values.Cash);

    public static readonly DualPricingAlternativeTender BankTransfer = new(Values.BankTransfer);

    public DualPricingAlternativeTender(string value)
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
    public static DualPricingAlternativeTender FromCustom(string value)
    {
        return new DualPricingAlternativeTender(value);
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

    public static bool operator ==(DualPricingAlternativeTender value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DualPricingAlternativeTender value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DualPricingAlternativeTender value) => value.Value;

    public static explicit operator DualPricingAlternativeTender(string value) => new(value);

    internal class DualPricingAlternativeTenderSerializer
        : JsonConverter<DualPricingAlternativeTender>
    {
        public override DualPricingAlternativeTender Read(
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
            return new DualPricingAlternativeTender(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DualPricingAlternativeTender value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override DualPricingAlternativeTender ReadAsPropertyName(
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
            return new DualPricingAlternativeTender(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            DualPricingAlternativeTender value,
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
        public const string Card = "card";

        public const string Cash = "cash";

        public const string BankTransfer = "bankTransfer";
    }
}
