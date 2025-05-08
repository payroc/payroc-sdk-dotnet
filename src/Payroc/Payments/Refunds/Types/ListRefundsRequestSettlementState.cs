using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Refunds;

[JsonConverter(typeof(StringEnumSerializer<ListRefundsRequestSettlementState>))]
public readonly record struct ListRefundsRequestSettlementState : IStringEnum
{
    public static readonly ListRefundsRequestSettlementState Settled = new(Values.Settled);

    public static readonly ListRefundsRequestSettlementState Unsettled = new(Values.Unsettled);

    public ListRefundsRequestSettlementState(string value)
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
    public static ListRefundsRequestSettlementState FromCustom(string value)
    {
        return new ListRefundsRequestSettlementState(value);
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

    public static bool operator ==(ListRefundsRequestSettlementState value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListRefundsRequestSettlementState value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListRefundsRequestSettlementState value) => value.Value;

    public static explicit operator ListRefundsRequestSettlementState(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Settled = "settled";

        public const string Unsettled = "unsettled";
    }
}
