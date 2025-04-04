using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardBalanceBenefitCategory>))]
public readonly record struct CardBalanceBenefitCategory : IStringEnum
{
    public static readonly CardBalanceBenefitCategory Cash = Custom(Values.Cash);

    public static readonly CardBalanceBenefitCategory FoodStamp = Custom(Values.FoodStamp);

    public CardBalanceBenefitCategory(string value)
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
    public static CardBalanceBenefitCategory Custom(string value)
    {
        return new CardBalanceBenefitCategory(value);
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

    public static bool operator ==(CardBalanceBenefitCategory value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CardBalanceBenefitCategory value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Cash = "cash";

        public const string FoodStamp = "foodStamp";
    }
}
