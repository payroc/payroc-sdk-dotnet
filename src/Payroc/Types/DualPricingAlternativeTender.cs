using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DualPricingAlternativeTender>))]
public readonly record struct DualPricingAlternativeTender : IStringEnum
{
    public static readonly DualPricingAlternativeTender Card = Custom(Values.Card);

    public static readonly DualPricingAlternativeTender Cash = Custom(Values.Cash);

    public static readonly DualPricingAlternativeTender BankTransfer = Custom(Values.BankTransfer);

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
    public static DualPricingAlternativeTender Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Card = "card";

        public const string Cash = "cash";

        public const string BankTransfer = "bankTransfer";
    }
}
