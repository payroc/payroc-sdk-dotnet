using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingAccountType>))]
public readonly record struct FundingAccountType : IStringEnum
{
    public static readonly FundingAccountType Checking = Custom(Values.Checking);

    public static readonly FundingAccountType Savings = Custom(Values.Savings);

    public static readonly FundingAccountType GeneralLedger = Custom(Values.GeneralLedger);

    public FundingAccountType(string value)
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
    public static FundingAccountType Custom(string value)
    {
        return new FundingAccountType(value);
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

    public static bool operator ==(FundingAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Checking = "checking";

        public const string Savings = "savings";

        public const string GeneralLedger = "generalLedger";
    }
}
