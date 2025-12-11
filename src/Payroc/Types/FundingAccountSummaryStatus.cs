using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingAccountSummaryStatus>))]
[Serializable]
public readonly record struct FundingAccountSummaryStatus : IStringEnum
{
    public static readonly FundingAccountSummaryStatus Approved = new(Values.Approved);

    public static readonly FundingAccountSummaryStatus Rejected = new(Values.Rejected);

    public static readonly FundingAccountSummaryStatus Pending = new(Values.Pending);

    public FundingAccountSummaryStatus(string value)
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
    public static FundingAccountSummaryStatus FromCustom(string value)
    {
        return new FundingAccountSummaryStatus(value);
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

    public static bool operator ==(FundingAccountSummaryStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingAccountSummaryStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FundingAccountSummaryStatus value) => value.Value;

    public static explicit operator FundingAccountSummaryStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Approved = "approved";

        public const string Rejected = "rejected";

        public const string Pending = "pending";
    }
}
