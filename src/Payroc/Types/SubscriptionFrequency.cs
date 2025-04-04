using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SubscriptionFrequency>))]
public readonly record struct SubscriptionFrequency : IStringEnum
{
    public static readonly SubscriptionFrequency Weekly = Custom(Values.Weekly);

    public static readonly SubscriptionFrequency Fortnightly = Custom(Values.Fortnightly);

    public static readonly SubscriptionFrequency Monthly = Custom(Values.Monthly);

    public static readonly SubscriptionFrequency Quarterly = Custom(Values.Quarterly);

    public static readonly SubscriptionFrequency Yearly = Custom(Values.Yearly);

    public SubscriptionFrequency(string value)
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
    public static SubscriptionFrequency Custom(string value)
    {
        return new SubscriptionFrequency(value);
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

    public static bool operator ==(SubscriptionFrequency value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionFrequency value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Weekly = "weekly";

        public const string Fortnightly = "fortnightly";

        public const string Monthly = "monthly";

        public const string Quarterly = "quarterly";

        public const string Yearly = "yearly";
    }
}
