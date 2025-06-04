using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.EventSubscriptions;

[JsonConverter(typeof(StringEnumSerializer<ListEventSubscriptionsRequestStatus>))]
public readonly record struct ListEventSubscriptionsRequestStatus : IStringEnum
{
    public static readonly ListEventSubscriptionsRequestStatus Registered = new(Values.Registered);

    public static readonly ListEventSubscriptionsRequestStatus Suspended = new(Values.Suspended);

    public static readonly ListEventSubscriptionsRequestStatus Failed = new(Values.Failed);

    public ListEventSubscriptionsRequestStatus(string value)
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
    public static ListEventSubscriptionsRequestStatus FromCustom(string value)
    {
        return new ListEventSubscriptionsRequestStatus(value);
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

    public static bool operator ==(ListEventSubscriptionsRequestStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListEventSubscriptionsRequestStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListEventSubscriptionsRequestStatus value) =>
        value.Value;

    public static explicit operator ListEventSubscriptionsRequestStatus(string value) => new(value);

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
