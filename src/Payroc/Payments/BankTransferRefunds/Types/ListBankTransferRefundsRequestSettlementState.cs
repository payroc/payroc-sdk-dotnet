using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

[JsonConverter(typeof(StringEnumSerializer<ListBankTransferRefundsRequestSettlementState>))]
[Serializable]
public readonly record struct ListBankTransferRefundsRequestSettlementState : IStringEnum
{
    public static readonly ListBankTransferRefundsRequestSettlementState Settled = new(
        Values.Settled
    );

    public static readonly ListBankTransferRefundsRequestSettlementState Unsettled = new(
        Values.Unsettled
    );

    public ListBankTransferRefundsRequestSettlementState(string value)
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
    public static ListBankTransferRefundsRequestSettlementState FromCustom(string value)
    {
        return new ListBankTransferRefundsRequestSettlementState(value);
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
        ListBankTransferRefundsRequestSettlementState value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListBankTransferRefundsRequestSettlementState value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListBankTransferRefundsRequestSettlementState value) =>
        value.Value;

    public static explicit operator ListBankTransferRefundsRequestSettlementState(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Settled = "settled";

        public const string Unsettled = "unsettled";
    }
}
