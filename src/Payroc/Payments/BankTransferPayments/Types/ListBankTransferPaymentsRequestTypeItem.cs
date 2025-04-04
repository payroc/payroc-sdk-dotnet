using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferPayments;

[JsonConverter(typeof(StringEnumSerializer<ListBankTransferPaymentsRequestTypeItem>))]
public readonly record struct ListBankTransferPaymentsRequestTypeItem : IStringEnum
{
    public static readonly ListBankTransferPaymentsRequestTypeItem Payment = Custom(Values.Payment);

    public static readonly ListBankTransferPaymentsRequestTypeItem AccountVerification = Custom(
        Values.AccountVerification
    );

    public ListBankTransferPaymentsRequestTypeItem(string value)
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
    public static ListBankTransferPaymentsRequestTypeItem Custom(string value)
    {
        return new ListBankTransferPaymentsRequestTypeItem(value);
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

    public static bool operator ==(ListBankTransferPaymentsRequestTypeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListBankTransferPaymentsRequestTypeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Payment = "payment";

        public const string AccountVerification = "accountVerification";
    }
}
