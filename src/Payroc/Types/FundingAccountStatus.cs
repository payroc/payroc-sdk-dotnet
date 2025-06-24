using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingAccountStatus>))]
[Serializable]
public readonly record struct FundingAccountStatus : IStringEnum
{
    public static readonly FundingAccountStatus Approved = new(Values.Approved);

    public static readonly FundingAccountStatus Rejected = new(Values.Rejected);

    public static readonly FundingAccountStatus Pending = new(Values.Pending);

    public static readonly FundingAccountStatus Hold = new(Values.Hold);

    public FundingAccountStatus(string value)
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
    public static FundingAccountStatus FromCustom(string value)
    {
        return new FundingAccountStatus(value);
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

    public static bool operator ==(FundingAccountStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingAccountStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FundingAccountStatus value) => value.Value;

    public static explicit operator FundingAccountStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Approved = "approved";

        public const string Rejected = "rejected";

        public const string Pending = "pending";

        public const string Hold = "hold";
    }
}
