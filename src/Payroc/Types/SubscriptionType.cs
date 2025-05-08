using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SubscriptionType>))]
public readonly record struct SubscriptionType : IStringEnum
{
    public static readonly SubscriptionType Manual = new(Values.Manual);

    public static readonly SubscriptionType Automatic = new(Values.Automatic);

    public SubscriptionType(string value)
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
    public static SubscriptionType FromCustom(string value)
    {
        return new SubscriptionType(value);
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

    public static bool operator ==(SubscriptionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SubscriptionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SubscriptionType value) => value.Value;

    public static explicit operator SubscriptionType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Manual = "manual";

        public const string Automatic = "automatic";
    }
}
