using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Refunds;

[JsonConverter(
    typeof(ListRefundsRequestSettlementState.ListRefundsRequestSettlementStateSerializer)
)]
[Serializable]
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

    internal class ListRefundsRequestSettlementStateSerializer
        : JsonConverter<ListRefundsRequestSettlementState>
    {
        public override ListRefundsRequestSettlementState Read(
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
            return new ListRefundsRequestSettlementState(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ListRefundsRequestSettlementState value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ListRefundsRequestSettlementState ReadAsPropertyName(
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
            return new ListRefundsRequestSettlementState(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ListRefundsRequestSettlementState value,
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
        public const string Settled = "settled";

        public const string Unsettled = "unsettled";
    }
}
