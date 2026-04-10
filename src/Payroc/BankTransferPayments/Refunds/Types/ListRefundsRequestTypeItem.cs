using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Refunds;

[JsonConverter(typeof(ListRefundsRequestTypeItem.ListRefundsRequestTypeItemSerializer))]
[Serializable]
public readonly record struct ListRefundsRequestTypeItem : IStringEnum
{
    public static readonly ListRefundsRequestTypeItem Refund = new(Values.Refund);

    public static readonly ListRefundsRequestTypeItem UnreferencedRefund = new(
        Values.UnreferencedRefund
    );

    public ListRefundsRequestTypeItem(string value)
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
    public static ListRefundsRequestTypeItem FromCustom(string value)
    {
        return new ListRefundsRequestTypeItem(value);
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

    public static bool operator ==(ListRefundsRequestTypeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListRefundsRequestTypeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListRefundsRequestTypeItem value) => value.Value;

    public static explicit operator ListRefundsRequestTypeItem(string value) => new(value);

    internal class ListRefundsRequestTypeItemSerializer : JsonConverter<ListRefundsRequestTypeItem>
    {
        public override ListRefundsRequestTypeItem Read(
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
            return new ListRefundsRequestTypeItem(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ListRefundsRequestTypeItem value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ListRefundsRequestTypeItem ReadAsPropertyName(
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
            return new ListRefundsRequestTypeItem(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ListRefundsRequestTypeItem value,
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
        public const string Refund = "refund";

        public const string UnreferencedRefund = "unreferencedRefund";
    }
}
