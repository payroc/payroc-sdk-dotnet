using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[JsonConverter(typeof(StringEnumSerializer<ListBankTransferPaymentsRequestStatusItem>))]
public readonly record struct ListBankTransferPaymentsRequestStatusItem : IStringEnum
{
    public static readonly ListBankTransferPaymentsRequestStatusItem Ready = new(Values.Ready);

    public static readonly ListBankTransferPaymentsRequestStatusItem Pending = new(Values.Pending);

    public static readonly ListBankTransferPaymentsRequestStatusItem Declined = new(
        Values.Declined
    );

    public static readonly ListBankTransferPaymentsRequestStatusItem Complete = new(
        Values.Complete
    );

    public static readonly ListBankTransferPaymentsRequestStatusItem Admin = new(Values.Admin);

    public static readonly ListBankTransferPaymentsRequestStatusItem Reversal = new(
        Values.Reversal
    );

    public static readonly ListBankTransferPaymentsRequestStatusItem Returned = new(
        Values.Returned
    );

    public ListBankTransferPaymentsRequestStatusItem(string value)
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
    public static ListBankTransferPaymentsRequestStatusItem FromCustom(string value)
    {
        return new ListBankTransferPaymentsRequestStatusItem(value);
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
        ListBankTransferPaymentsRequestStatusItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBankTransferPaymentsRequestStatusItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBankTransferPaymentsRequestStatusItem value) =>
        value.Value;

    public static explicit operator ListBankTransferPaymentsRequestStatusItem(string value) =>
        new(value);

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
