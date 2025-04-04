using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentSummaryStatus>))]
public readonly record struct PaymentSummaryStatus : IStringEnum
{
    public static readonly PaymentSummaryStatus Ready = Custom(Values.Ready);

    public static readonly PaymentSummaryStatus Pending = Custom(Values.Pending);

    public static readonly PaymentSummaryStatus Declined = Custom(Values.Declined);

    public static readonly PaymentSummaryStatus Complete = Custom(Values.Complete);

    public static readonly PaymentSummaryStatus Referral = Custom(Values.Referral);

    public static readonly PaymentSummaryStatus Pickup = Custom(Values.Pickup);

    public static readonly PaymentSummaryStatus Reversal = Custom(Values.Reversal);

    public static readonly PaymentSummaryStatus Returned = Custom(Values.Returned);

    public static readonly PaymentSummaryStatus Admin = Custom(Values.Admin);

    public static readonly PaymentSummaryStatus Expired = Custom(Values.Expired);

    public static readonly PaymentSummaryStatus Accepted = Custom(Values.Accepted);

    public static readonly PaymentSummaryStatus Review = Custom(Values.Review);

    public PaymentSummaryStatus(string value)
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
    public static PaymentSummaryStatus Custom(string value)
    {
        return new PaymentSummaryStatus(value);
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

    public static bool operator ==(PaymentSummaryStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentSummaryStatus value1, string value2) =>
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
