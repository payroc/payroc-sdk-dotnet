using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingRecipientFundingAccountsItemStatus>))]
public readonly record struct FundingRecipientFundingAccountsItemStatus : IStringEnum
{
    public static readonly FundingRecipientFundingAccountsItemStatus Approved = new(
        Values.Approved
    );

    public static readonly FundingRecipientFundingAccountsItemStatus Rejected = new(
        Values.Rejected
    );

    public static readonly FundingRecipientFundingAccountsItemStatus Pending = new(Values.Pending);

    public static readonly FundingRecipientFundingAccountsItemStatus Hold = new(Values.Hold);

    public FundingRecipientFundingAccountsItemStatus(string value)
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
    public static FundingRecipientFundingAccountsItemStatus FromCustom(string value)
    {
        return new FundingRecipientFundingAccountsItemStatus(value);
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

    public static bool operator ==(
        FundingRecipientFundingAccountsItemStatus value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        FundingRecipientFundingAccountsItemStatus value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(FundingRecipientFundingAccountsItemStatus value) =>
        value.Value;

    public static explicit operator FundingRecipientFundingAccountsItemStatus(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Approved = "approved";

        public const string Rejected = "rejected";

        public const string Pending = "pending";

        public const string Hold = "hold";
    }
}
