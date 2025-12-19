using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Refunds;

[JsonConverter(typeof(StringEnumSerializer<ListRefundsRequestTypeItem>))]
[Serializable]
public readonly record struct ListRefundsRequestTypeItem : IStringEnum
{
    public static readonly ListRefundsRequestTypeItem Refund = new(Values.Refund);

    public static readonly ListRefundsRequestTypeItem UnreferencedRefund = new(
        Values.UnreferencedRefund
    );

    public ListRefundsRequestTypeItem(string value)
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
    public static ListRefundsRequestTypeItem FromCustom(string value)
    {
        return new ListRefundsRequestTypeItem(value);
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

    public static bool operator ==(ListRefundsRequestTypeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListRefundsRequestTypeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListRefundsRequestTypeItem value) => value.Value;

    public static explicit operator ListRefundsRequestTypeItem(string value) => new(value);

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
