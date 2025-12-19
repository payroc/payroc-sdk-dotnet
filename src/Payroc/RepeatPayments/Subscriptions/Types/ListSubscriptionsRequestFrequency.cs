using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.RepeatPayments.Subscriptions;

[JsonConverter(typeof(StringEnumSerializer<ListSubscriptionsRequestFrequency>))]
[Serializable]
public readonly record struct ListSubscriptionsRequestFrequency : IStringEnum
{
    public static readonly ListSubscriptionsRequestFrequency Weekly = new(Values.Weekly);

    public static readonly ListSubscriptionsRequestFrequency Fortnightly = new(Values.Fortnightly);

    public static readonly ListSubscriptionsRequestFrequency Monthly = new(Values.Monthly);

    public static readonly ListSubscriptionsRequestFrequency Quarterly = new(Values.Quarterly);

    public static readonly ListSubscriptionsRequestFrequency Yearly = new(Values.Yearly);

    public ListSubscriptionsRequestFrequency(string value)
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
    public static ListSubscriptionsRequestFrequency FromCustom(string value)
    {
        return new ListSubscriptionsRequestFrequency(value);
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

    public static bool operator ==(ListSubscriptionsRequestFrequency value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListSubscriptionsRequestFrequency value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListSubscriptionsRequestFrequency value) => value.Value;

    public static explicit operator ListSubscriptionsRequestFrequency(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Weekly = "weekly";

        public const string Fortnightly = "fortnightly";

        public const string Monthly = "monthly";

        public const string Quarterly = "quarterly";

        public const string Yearly = "yearly";
    }
}
