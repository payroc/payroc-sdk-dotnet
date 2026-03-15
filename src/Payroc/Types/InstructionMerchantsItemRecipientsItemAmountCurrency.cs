using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(InstructionMerchantsItemRecipientsItemAmountCurrency.InstructionMerchantsItemRecipientsItemAmountCurrencySerializer)
)]
[Serializable]
public readonly record struct InstructionMerchantsItemRecipientsItemAmountCurrency : IStringEnum
{
    public static readonly InstructionMerchantsItemRecipientsItemAmountCurrency Usd = new(
        Values.Usd
    );

    public InstructionMerchantsItemRecipientsItemAmountCurrency(string value)
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
    public static InstructionMerchantsItemRecipientsItemAmountCurrency FromCustom(string value)
    {
        return new InstructionMerchantsItemRecipientsItemAmountCurrency(value);
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

    public static bool operator ==(
        InstructionMerchantsItemRecipientsItemAmountCurrency value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        InstructionMerchantsItemRecipientsItemAmountCurrency value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        InstructionMerchantsItemRecipientsItemAmountCurrency value
    ) => value.Value;

    public static explicit operator InstructionMerchantsItemRecipientsItemAmountCurrency(
        string value
    ) => new(value);

    internal class InstructionMerchantsItemRecipientsItemAmountCurrencySerializer
        : JsonConverter<InstructionMerchantsItemRecipientsItemAmountCurrency>
    {
        public override InstructionMerchantsItemRecipientsItemAmountCurrency Read(
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
            return new InstructionMerchantsItemRecipientsItemAmountCurrency(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InstructionMerchantsItemRecipientsItemAmountCurrency value,
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
        public const string Usd = "USD";
    }
}
