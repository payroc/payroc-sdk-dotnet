using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

[JsonConverter(typeof(StringEnumSerializer<ListBankTransferRefundsRequestStatusItem>))]
public readonly record struct ListBankTransferRefundsRequestStatusItem : IStringEnum
{
    public static readonly ListBankTransferRefundsRequestStatusItem Ready = Custom(Values.Ready);

    public static readonly ListBankTransferRefundsRequestStatusItem Pending = Custom(
        Values.Pending
    );

    public static readonly ListBankTransferRefundsRequestStatusItem Declined = Custom(
        Values.Declined
    );

    public static readonly ListBankTransferRefundsRequestStatusItem Complete = Custom(
        Values.Complete
    );

    public static readonly ListBankTransferRefundsRequestStatusItem Admin = Custom(Values.Admin);

    public static readonly ListBankTransferRefundsRequestStatusItem Reversal = Custom(
        Values.Reversal
    );

    public static readonly ListBankTransferRefundsRequestStatusItem Returned = Custom(
        Values.Returned
    );

    public ListBankTransferRefundsRequestStatusItem(string value)
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
    public static ListBankTransferRefundsRequestStatusItem Custom(string value)
    {
        return new ListBankTransferRefundsRequestStatusItem(value);
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
        ListBankTransferRefundsRequestStatusItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBankTransferRefundsRequestStatusItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Ready = "ready";

        public const string Pending = "pending";

        public const string Declined = "declined";

        public const string Complete = "complete";

        public const string Admin = "admin";

        public const string Reversal = "reversal";

        public const string Returned = "returned";
    }
}
