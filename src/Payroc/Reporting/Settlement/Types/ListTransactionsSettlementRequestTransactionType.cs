using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Reporting.Settlement;

[JsonConverter(
    typeof(ListTransactionsSettlementRequestTransactionType.ListTransactionsSettlementRequestTransactionTypeSerializer)
)]
[Serializable]
public readonly record struct ListTransactionsSettlementRequestTransactionType : IStringEnum
{
    public static readonly ListTransactionsSettlementRequestTransactionType Capture = new(
        Values.Capture
    );

    public static readonly ListTransactionsSettlementRequestTransactionType Return = new(
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
    public static ListTransactionsSettlementRequestTransactionType FromCustom(string value)
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

    public static explicit operator string(
        ListTransactionsSettlementRequestTransactionType value
    ) => value.Value;

    public static explicit operator ListTransactionsSettlementRequestTransactionType(
        string value
    ) => new(value);

    internal class ListTransactionsSettlementRequestTransactionTypeSerializer
        : JsonConverter<ListTransactionsSettlementRequestTransactionType>
    {
        public override ListTransactionsSettlementRequestTransactionType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new ListTransactionsSettlementRequestTransactionType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ListTransactionsSettlementRequestTransactionType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ListTransactionsSettlementRequestTransactionType ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new ListTransactionsSettlementRequestTransactionType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ListTransactionsSettlementRequestTransactionType value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Capture = "Capture";

        public const string Return = "Return";
    }
}
