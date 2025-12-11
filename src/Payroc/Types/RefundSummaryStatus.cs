using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RefundSummaryStatus>))]
[Serializable]
public readonly record struct RefundSummaryStatus : IStringEnum
{
    public static readonly RefundSummaryStatus Ready = new(Values.Ready);

    public static readonly RefundSummaryStatus Pending = new(Values.Pending);

    public static readonly RefundSummaryStatus Declined = new(Values.Declined);

    public static readonly RefundSummaryStatus Complete = new(Values.Complete);

    public static readonly RefundSummaryStatus Referral = new(Values.Referral);

    public static readonly RefundSummaryStatus Pickup = new(Values.Pickup);

    public static readonly RefundSummaryStatus Reversal = new(Values.Reversal);

    public static readonly RefundSummaryStatus Returned = new(Values.Returned);

    public static readonly RefundSummaryStatus Admin = new(Values.Admin);

    public static readonly RefundSummaryStatus Expired = new(Values.Expired);

    public static readonly RefundSummaryStatus Accepted = new(Values.Accepted);

    public RefundSummaryStatus(string value)
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
    public static RefundSummaryStatus FromCustom(string value)
    {
        return new RefundSummaryStatus(value);
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

    public static bool operator ==(RefundSummaryStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RefundSummaryStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RefundSummaryStatus value) => value.Value;

    public static explicit operator RefundSummaryStatus(string value) => new(value);

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

        public const string Returned = "returned";

        public const string Admin = "admin";

        public const string Expired = "expired";

        public const string Accepted = "accepted";
    }
}
