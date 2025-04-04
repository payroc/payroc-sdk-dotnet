using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardPayloadAccountType>))]
public readonly record struct CardPayloadAccountType : IStringEnum
{
    public static readonly CardPayloadAccountType Checking = Custom(Values.Checking);

    public static readonly CardPayloadAccountType Savings = Custom(Values.Savings);

    public CardPayloadAccountType(string value)
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
    public static CardPayloadAccountType Custom(string value)
    {
        return new CardPayloadAccountType(value);
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

    public static bool operator ==(CardPayloadAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CardPayloadAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Checking = "checking";

        public const string Savings = "savings";
    }
}
