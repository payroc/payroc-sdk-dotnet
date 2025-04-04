using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingAccountUse>))]
public readonly record struct FundingAccountUse : IStringEnum
{
    public static readonly FundingAccountUse Credit = Custom(Values.Credit);

    public static readonly FundingAccountUse Debit = Custom(Values.Debit);

    public static readonly FundingAccountUse CreditAndDebit = Custom(Values.CreditAndDebit);

    public FundingAccountUse(string value)
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
    public static FundingAccountUse Custom(string value)
    {
        return new FundingAccountUse(value);
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

    public static bool operator ==(FundingAccountUse value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingAccountUse value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Credit = "credit";

        public const string Debit = "debit";

        public const string CreditAndDebit = "creditAndDebit";
    }
}
