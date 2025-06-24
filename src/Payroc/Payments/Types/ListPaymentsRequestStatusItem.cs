using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestStatusItem>))]
[Serializable]
public readonly record struct ListPaymentsRequestStatusItem : IStringEnum
{
    public static readonly ListPaymentsRequestStatusItem Ready = new(Values.Ready);

    public static readonly ListPaymentsRequestStatusItem Pending = new(Values.Pending);

    public static readonly ListPaymentsRequestStatusItem Declined = new(Values.Declined);

    public static readonly ListPaymentsRequestStatusItem Complete = new(Values.Complete);

    public static readonly ListPaymentsRequestStatusItem Referral = new(Values.Referral);

    public static readonly ListPaymentsRequestStatusItem Pickup = new(Values.Pickup);

    public static readonly ListPaymentsRequestStatusItem Reversal = new(Values.Reversal);

    public static readonly ListPaymentsRequestStatusItem Admin = new(Values.Admin);

    public static readonly ListPaymentsRequestStatusItem Expired = new(Values.Expired);

    public static readonly ListPaymentsRequestStatusItem Accepted = new(Values.Accepted);

    public ListPaymentsRequestStatusItem(string value)
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
    public static ListPaymentsRequestStatusItem FromCustom(string value)
    {
        return new ListPaymentsRequestStatusItem(value);
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

    public static bool operator ==(ListPaymentsRequestStatusItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentsRequestStatusItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPaymentsRequestStatusItem value) => value.Value;

    public static explicit operator ListPaymentsRequestStatusItem(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Ready = "ready";

        public const string Pending = "pending";

        public const string Declined = "declined";

        public const string Complete = "complete";

        public const string Referral = "referral";

        public const string Pickup = "pickup";

        public const string Reversal = "reversal";

        public const string Admin = "admin";

        public const string Expired = "expired";

        public const string Accepted = "accepted";
    }
}
