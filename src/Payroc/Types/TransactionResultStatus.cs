using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionResultStatus>))]
public readonly record struct TransactionResultStatus : IStringEnum
{
    public static readonly TransactionResultStatus Ready = Custom(Values.Ready);

    public static readonly TransactionResultStatus Pending = Custom(Values.Pending);

    public static readonly TransactionResultStatus Declined = Custom(Values.Declined);

    public static readonly TransactionResultStatus Complete = Custom(Values.Complete);

    public static readonly TransactionResultStatus Referral = Custom(Values.Referral);

    public static readonly TransactionResultStatus Pickup = Custom(Values.Pickup);

    public static readonly TransactionResultStatus Reversal = Custom(Values.Reversal);

    public static readonly TransactionResultStatus Admin = Custom(Values.Admin);

    public static readonly TransactionResultStatus Expired = Custom(Values.Expired);

    public static readonly TransactionResultStatus Accepted = Custom(Values.Accepted);

    public static readonly TransactionResultStatus Review = Custom(Values.Review);

    public TransactionResultStatus(string value)
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
    public static TransactionResultStatus Custom(string value)
    {
        return new TransactionResultStatus(value);
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

    public static bool operator ==(TransactionResultStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionResultStatus value1, string value2) =>
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
