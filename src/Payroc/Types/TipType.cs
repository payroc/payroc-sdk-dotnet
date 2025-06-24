using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TipType>))]
[Serializable]
public readonly record struct TipType : IStringEnum
{
    public static readonly TipType Percentage = new(Values.Percentage);

    public static readonly TipType FixedAmount = new(Values.FixedAmount);

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
    public static TipType FromCustom(string value)
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

    public static explicit operator string(TipType value) => value.Value;

    public static explicit operator TipType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Percentage = "percentage";

        public const string FixedAmount = "fixedAmount";
    }
}
