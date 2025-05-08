using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Refunds;

[JsonConverter(typeof(StringEnumSerializer<ListRefundsRequestStatusItem>))]
public readonly record struct ListRefundsRequestStatusItem : IStringEnum
{
    public static readonly ListRefundsRequestStatusItem Ready = new(Values.Ready);

    public static readonly ListRefundsRequestStatusItem Pending = new(Values.Pending);

    public static readonly ListRefundsRequestStatusItem Declined = new(Values.Declined);

    public static readonly ListRefundsRequestStatusItem Complete = new(Values.Complete);

    public static readonly ListRefundsRequestStatusItem Referral = new(Values.Referral);

    public static readonly ListRefundsRequestStatusItem Pickup = new(Values.Pickup);

    public static readonly ListRefundsRequestStatusItem Reversal = new(Values.Reversal);

    public static readonly ListRefundsRequestStatusItem Admin = new(Values.Admin);

    public static readonly ListRefundsRequestStatusItem Expired = new(Values.Expired);

    public static readonly ListRefundsRequestStatusItem Accepted = new(Values.Accepted);

    public static readonly ListRefundsRequestStatusItem Review = new(Values.Review);

    public ListRefundsRequestStatusItem(string value)
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
    public static ListRefundsRequestStatusItem FromCustom(string value)
    {
        return new ListRefundsRequestStatusItem(value);
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

    public static bool operator ==(ListRefundsRequestStatusItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListRefundsRequestStatusItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListRefundsRequestStatusItem value) => value.Value;

    public static explicit operator ListRefundsRequestStatusItem(string value) => new(value);

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
