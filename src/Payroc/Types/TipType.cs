using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TipType>))]
public readonly record struct TipType : IStringEnum
{
    public static readonly TipType Percentage = Custom(Values.Percentage);

    public static readonly TipType FixedAmount = Custom(Values.FixedAmount);

    public TipType(string value)
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
    public static TipType Custom(string value)
    {
        return new TipType(value);
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

    public static bool operator ==(TipType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(TipType value1, string value2) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Percentage = "percentage";

        public const string FixedAmount = "fixedAmount";
    }
}
