using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SubscriptionStateStatus>))]
public readonly record struct SubscriptionStateStatus : IStringEnum
{
    public static readonly SubscriptionStateStatus Active = Custom(Values.Active);

    public static readonly SubscriptionStateStatus Completed = Custom(Values.Completed);

    public static readonly SubscriptionStateStatus Suspended = Custom(Values.Suspended);

    public static readonly SubscriptionStateStatus Cancelled = Custom(Values.Cancelled);

    public SubscriptionStateStatus(string value)
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
    public static SubscriptionStateStatus Custom(string value)
    {
        return new SubscriptionStateStatus(value);
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

    public static bool operator ==(SubscriptionStateStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionStateStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Active = "active";

        public const string Completed = "completed";

        public const string Suspended = "suspended";

        public const string Cancelled = "cancelled";
    }
}
