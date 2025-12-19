using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<InstructionMerchantsItemRecipientsItemAmountCurrency>))]
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Usd = "USD";
    }
}
