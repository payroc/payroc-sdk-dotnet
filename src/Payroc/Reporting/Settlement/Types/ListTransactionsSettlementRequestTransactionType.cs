using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[JsonConverter(typeof(StringEnumSerializer<ListTransactionsSettlementRequestTransactionType>))]
public readonly record struct ListTransactionsSettlementRequestTransactionType : IStringEnum
{
    public static readonly ListTransactionsSettlementRequestTransactionType Capture = Custom(
        Values.Capture
    );

    public static readonly ListTransactionsSettlementRequestTransactionType Return = Custom(
        Values.Return
    );

    public ListTransactionsSettlementRequestTransactionType(string value)
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
    public static ListTransactionsSettlementRequestTransactionType Custom(string value)
    {
        return new ListTransactionsSettlementRequestTransactionType(value);
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
        ListTransactionsSettlementRequestTransactionType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListTransactionsSettlementRequestTransactionType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Capture = "Capture";

        public const string Return = "Return";
    }
}
