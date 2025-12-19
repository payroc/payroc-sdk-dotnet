using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestTypeItem>))]
[Serializable]
public readonly record struct ListPaymentsRequestTypeItem : IStringEnum
{
    public static readonly ListPaymentsRequestTypeItem Payment = new(Values.Payment);

    public static readonly ListPaymentsRequestTypeItem AccountVerification = new(
        Values.AccountVerification
    );

    public ListPaymentsRequestTypeItem(string value)
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
    public static ListPaymentsRequestTypeItem FromCustom(string value)
    {
        return new ListPaymentsRequestTypeItem(value);
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

    public static bool operator ==(ListPaymentsRequestTypeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentsRequestTypeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPaymentsRequestTypeItem value) => value.Value;

    public static explicit operator ListPaymentsRequestTypeItem(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Payment = "payment";

        public const string AccountVerification = "accountVerification";
    }
}
