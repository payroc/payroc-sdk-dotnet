using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.RepeatPayments.Subscriptions;

[JsonConverter(typeof(StringEnumSerializer<ListSubscriptionsRequestStatus>))]
[Serializable]
public readonly record struct ListSubscriptionsRequestStatus : IStringEnum
{
    public static readonly ListSubscriptionsRequestStatus Active = new(Values.Active);

    public static readonly ListSubscriptionsRequestStatus Completed = new(Values.Completed);

    public static readonly ListSubscriptionsRequestStatus Suspended = new(Values.Suspended);

    public static readonly ListSubscriptionsRequestStatus Cancelled = new(Values.Cancelled);

    public ListSubscriptionsRequestStatus(string value)
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
    public static ListSubscriptionsRequestStatus FromCustom(string value)
    {
        return new ListSubscriptionsRequestStatus(value);
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

    public static bool operator ==(ListSubscriptionsRequestStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListSubscriptionsRequestStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListSubscriptionsRequestStatus value) => value.Value;

    public static explicit operator ListSubscriptionsRequestStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Active = "active";

        public const string Completed = "completed";

        public const string Suspended = "suspended";

        public const string Cancelled = "cancelled";
    }
}
