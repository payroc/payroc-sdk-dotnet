using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SingleUseTokenPayloadAccountType>))]
public readonly record struct SingleUseTokenPayloadAccountType : IStringEnum
{
    public static readonly SingleUseTokenPayloadAccountType Checking = Custom(Values.Checking);

    public static readonly SingleUseTokenPayloadAccountType Savings = Custom(Values.Savings);

    public SingleUseTokenPayloadAccountType(string value)
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
    public static SingleUseTokenPayloadAccountType Custom(string value)
    {
        return new SingleUseTokenPayloadAccountType(value);
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

    public static bool operator ==(SingleUseTokenPayloadAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUseTokenPayloadAccountType value1, string value2) =>
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
