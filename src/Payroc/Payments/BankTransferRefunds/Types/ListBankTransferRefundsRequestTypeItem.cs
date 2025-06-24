using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

[JsonConverter(typeof(StringEnumSerializer<ListBankTransferRefundsRequestTypeItem>))]
[Serializable]
public readonly record struct ListBankTransferRefundsRequestTypeItem : IStringEnum
{
    public static readonly ListBankTransferRefundsRequestTypeItem Refund = new(Values.Refund);

    public static readonly ListBankTransferRefundsRequestTypeItem UnreferencedRefund = new(
        Values.UnreferencedRefund
    );

    public ListBankTransferRefundsRequestTypeItem(string value)
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
    public static ListBankTransferRefundsRequestTypeItem FromCustom(string value)
    {
        return new ListBankTransferRefundsRequestTypeItem(value);
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

    public static bool operator ==(ListBankTransferRefundsRequestTypeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListBankTransferRefundsRequestTypeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListBankTransferRefundsRequestTypeItem value) =>
        value.Value;

    public static explicit operator ListBankTransferRefundsRequestTypeItem(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Refund = "refund";

        public const string UnreferencedRefund = "unreferencedRefund";
    }
}
