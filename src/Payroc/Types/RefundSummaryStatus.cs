using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RefundSummaryStatus>))]
public readonly record struct RefundSummaryStatus : IStringEnum
{
    public static readonly RefundSummaryStatus Ready = Custom(Values.Ready);

    public static readonly RefundSummaryStatus Pending = Custom(Values.Pending);

    public static readonly RefundSummaryStatus Declined = Custom(Values.Declined);

    public static readonly RefundSummaryStatus Complete = Custom(Values.Complete);

    public static readonly RefundSummaryStatus Referral = Custom(Values.Referral);

    public static readonly RefundSummaryStatus Pickup = Custom(Values.Pickup);

    public static readonly RefundSummaryStatus Reversal = Custom(Values.Reversal);

    public static readonly RefundSummaryStatus Returned = Custom(Values.Returned);

    public static readonly RefundSummaryStatus Admin = Custom(Values.Admin);

    public static readonly RefundSummaryStatus Expired = Custom(Values.Expired);

    public static readonly RefundSummaryStatus Accepted = Custom(Values.Accepted);

    public static readonly RefundSummaryStatus Review = Custom(Values.Review);

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
    public static RefundSummaryStatus Custom(string value)
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

        public const string Returned = "returned";

        public const string Admin = "admin";

        public const string Expired = "expired";

        public const string Accepted = "accepted";

        public const string Review = "review";
    }
}
