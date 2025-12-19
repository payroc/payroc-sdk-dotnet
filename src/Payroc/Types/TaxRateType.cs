using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TaxRateType>))]
[Serializable]
public readonly record struct TaxRateType : IStringEnum
{
    public static readonly TaxRateType Rate = new(Values.Rate);

    public TaxRateType(string value)
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
    public static TaxRateType FromCustom(string value)
    {
        return new TaxRateType(value);
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

    public static bool operator ==(TaxRateType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TaxRateType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TaxRateType value) => value.Value;

    public static explicit operator TaxRateType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Rate = "rate";
    }
}
