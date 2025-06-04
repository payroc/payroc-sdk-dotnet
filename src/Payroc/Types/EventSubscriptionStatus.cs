using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<EventSubscriptionStatus>))]
public readonly record struct EventSubscriptionStatus : IStringEnum
{
    public static readonly EventSubscriptionStatus Registered = new(Values.Registered);

    public static readonly EventSubscriptionStatus Suspended = new(Values.Suspended);

    public static readonly EventSubscriptionStatus Failed = new(Values.Failed);

    public EventSubscriptionStatus(string value)
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
    public static EventSubscriptionStatus FromCustom(string value)
    {
        return new EventSubscriptionStatus(value);
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

    public static bool operator ==(EventSubscriptionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EventSubscriptionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EventSubscriptionStatus value) => value.Value;

    public static explicit operator EventSubscriptionStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Registered = "registered";

        public const string Suspended = "suspended";

        public const string Failed = "failed";
    }
}
