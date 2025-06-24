using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestTipModeItem>))]
[Serializable]
public readonly record struct ListPaymentsRequestTipModeItem : IStringEnum
{
    public static readonly ListPaymentsRequestTipModeItem NoTip = new(Values.NoTip);

    public static readonly ListPaymentsRequestTipModeItem Prompted = new(Values.Prompted);

    public static readonly ListPaymentsRequestTipModeItem Adjusted = new(Values.Adjusted);

    public ListPaymentsRequestTipModeItem(string value)
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
    public static ListPaymentsRequestTipModeItem FromCustom(string value)
    {
        return new ListPaymentsRequestTipModeItem(value);
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

    public static bool operator ==(ListPaymentsRequestTipModeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentsRequestTipModeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPaymentsRequestTipModeItem value) => value.Value;

    public static explicit operator ListPaymentsRequestTipModeItem(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string NoTip = "noTip";

        public const string Prompted = "prompted";

        public const string Adjusted = "adjusted";
    }
}
