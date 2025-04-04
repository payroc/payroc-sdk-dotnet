using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BaseUsAnnualFeeBillInMonth>))]
public readonly record struct BaseUsAnnualFeeBillInMonth : IStringEnum
{
    public static readonly BaseUsAnnualFeeBillInMonth June = Custom(Values.June);

    public static readonly BaseUsAnnualFeeBillInMonth December = Custom(Values.December);

    public BaseUsAnnualFeeBillInMonth(string value)
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
    public static BaseUsAnnualFeeBillInMonth Custom(string value)
    {
        return new BaseUsAnnualFeeBillInMonth(value);
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

    public static bool operator ==(BaseUsAnnualFeeBillInMonth value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BaseUsAnnualFeeBillInMonth value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string June = "june";

        public const string December = "december";
    }
}
