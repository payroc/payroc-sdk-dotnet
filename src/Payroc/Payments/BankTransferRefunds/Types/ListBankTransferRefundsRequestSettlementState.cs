using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.BankTransferRefunds;

[JsonConverter(typeof(StringEnumSerializer<ListBankTransferRefundsRequestSettlementState>))]
public readonly record struct ListBankTransferRefundsRequestSettlementState : IStringEnum
{
    public static readonly ListBankTransferRefundsRequestSettlementState Settled = Custom(
        Values.Settled
    );

    public static readonly ListBankTransferRefundsRequestSettlementState Unsettled = Custom(
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
    public static ListBankTransferRefundsRequestSettlementState Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Settled = "settled";

        public const string Unsettled = "unsettled";
    }
}
