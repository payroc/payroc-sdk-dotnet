using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[JsonConverter(typeof(StringEnumSerializer<ListTerminalOrdersProcessingAccountsRequestStatus>))]
[Serializable]
public readonly record struct ListTerminalOrdersProcessingAccountsRequestStatus : IStringEnum
{
    public static readonly ListTerminalOrdersProcessingAccountsRequestStatus Open = new(
        Values.Open
    );

    public static readonly ListTerminalOrdersProcessingAccountsRequestStatus Held = new(
        Values.Held
    );

    public static readonly ListTerminalOrdersProcessingAccountsRequestStatus Dispatched = new(
        Values.Dispatched
    );

    public static readonly ListTerminalOrdersProcessingAccountsRequestStatus Fulfilled = new(
        Values.Fulfilled
    );

    public static readonly ListTerminalOrdersProcessingAccountsRequestStatus Cancelled = new(
        Values.Cancelled
    );

    public ListTerminalOrdersProcessingAccountsRequestStatus(string value)
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
    public static ListTerminalOrdersProcessingAccountsRequestStatus FromCustom(string value)
    {
        return new ListTerminalOrdersProcessingAccountsRequestStatus(value);
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

    public static bool operator ==(
        ListTerminalOrdersProcessingAccountsRequestStatus value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListTerminalOrdersProcessingAccountsRequestStatus value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ListTerminalOrdersProcessingAccountsRequestStatus value
    ) => value.Value;

    public static explicit operator ListTerminalOrdersProcessingAccountsRequestStatus(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Open = "open";

        public const string Held = "held";

        public const string Dispatched = "dispatched";

        public const string Fulfilled = "fulfilled";

        public const string Cancelled = "cancelled";
    }
}
