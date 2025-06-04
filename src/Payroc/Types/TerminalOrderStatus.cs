using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TerminalOrderStatus>))]
public readonly record struct TerminalOrderStatus : IStringEnum
{
    public static readonly TerminalOrderStatus Open = new(Values.Open);

    public static readonly TerminalOrderStatus Held = new(Values.Held);

    public static readonly TerminalOrderStatus Dispatched = new(Values.Dispatched);

    public static readonly TerminalOrderStatus Fulfilled = new(Values.Fulfilled);

    public static readonly TerminalOrderStatus Cancelled = new(Values.Cancelled);

    public TerminalOrderStatus(string value)
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
    public static TerminalOrderStatus FromCustom(string value)
    {
        return new TerminalOrderStatus(value);
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

    public static bool operator ==(TerminalOrderStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TerminalOrderStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TerminalOrderStatus value) => value.Value;

    public static explicit operator TerminalOrderStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Open = "open";

        public const string Held = "held";

        public const string Dispatched = "dispatched";

        public const string Fulfilled = "fulfilled";

        public const string Cancelled = "cancelled";
    }
}
