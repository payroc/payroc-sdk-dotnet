using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DigitalWalletPayloadAccountType>))]
public readonly record struct DigitalWalletPayloadAccountType : IStringEnum
{
    public static readonly DigitalWalletPayloadAccountType Checking = Custom(Values.Checking);

    public static readonly DigitalWalletPayloadAccountType Savings = Custom(Values.Savings);

    public DigitalWalletPayloadAccountType(string value)
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
    public static DigitalWalletPayloadAccountType Custom(string value)
    {
        return new DigitalWalletPayloadAccountType(value);
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

    public static bool operator ==(DigitalWalletPayloadAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DigitalWalletPayloadAccountType value1, string value2) =>
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
