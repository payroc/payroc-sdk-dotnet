using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestStatusItem>))]
public readonly record struct ListPaymentsRequestStatusItem : IStringEnum
{
    public static readonly ListPaymentsRequestStatusItem Ready = Custom(Values.Ready);

    public static readonly ListPaymentsRequestStatusItem Pending = Custom(Values.Pending);

    public static readonly ListPaymentsRequestStatusItem Declined = Custom(Values.Declined);

    public static readonly ListPaymentsRequestStatusItem Complete = Custom(Values.Complete);

    public static readonly ListPaymentsRequestStatusItem Referral = Custom(Values.Referral);

    public static readonly ListPaymentsRequestStatusItem Pickup = Custom(Values.Pickup);

    public static readonly ListPaymentsRequestStatusItem Reversal = Custom(Values.Reversal);

    public static readonly ListPaymentsRequestStatusItem Admin = Custom(Values.Admin);

    public static readonly ListPaymentsRequestStatusItem Expired = Custom(Values.Expired);

    public static readonly ListPaymentsRequestStatusItem Accepted = Custom(Values.Accepted);

    public static readonly ListPaymentsRequestStatusItem Review = Custom(Values.Review);

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
    public static ListPaymentsRequestStatusItem Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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

        public const string Review = "review";
    }
}
