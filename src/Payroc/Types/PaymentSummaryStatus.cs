using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentSummaryStatus>))]
[Serializable]
public readonly record struct PaymentSummaryStatus : IStringEnum
{
    public static readonly PaymentSummaryStatus Ready = new(Values.Ready);

    public static readonly PaymentSummaryStatus Pending = new(Values.Pending);

    public static readonly PaymentSummaryStatus Declined = new(Values.Declined);

    public static readonly PaymentSummaryStatus Complete = new(Values.Complete);

    public static readonly PaymentSummaryStatus Referral = new(Values.Referral);

    public static readonly PaymentSummaryStatus Pickup = new(Values.Pickup);

    public static readonly PaymentSummaryStatus Reversal = new(Values.Reversal);

    public static readonly PaymentSummaryStatus Returned = new(Values.Returned);

    public static readonly PaymentSummaryStatus Admin = new(Values.Admin);

    public static readonly PaymentSummaryStatus Expired = new(Values.Expired);

    public static readonly PaymentSummaryStatus Accepted = new(Values.Accepted);

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
    public static PaymentSummaryStatus FromCustom(string value)
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

    public static explicit operator string(PaymentSummaryStatus value) => value.Value;

    public static explicit operator PaymentSummaryStatus(string value) => new(value);

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
