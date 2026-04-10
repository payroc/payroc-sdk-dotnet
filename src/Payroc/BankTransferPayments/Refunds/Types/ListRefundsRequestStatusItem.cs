using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.BankTransferPayments.Refunds;

[JsonConverter(typeof(ListRefundsRequestStatusItem.ListRefundsRequestStatusItemSerializer))]
[Serializable]
public readonly record struct ListRefundsRequestStatusItem : IStringEnum
{
    public static readonly ListRefundsRequestStatusItem Ready = new(Values.Ready);

    public static readonly ListRefundsRequestStatusItem Pending = new(Values.Pending);

    public static readonly ListRefundsRequestStatusItem Declined = new(Values.Declined);

    public static readonly ListRefundsRequestStatusItem Complete = new(Values.Complete);

    public static readonly ListRefundsRequestStatusItem Admin = new(Values.Admin);

    public static readonly ListRefundsRequestStatusItem Reversal = new(Values.Reversal);

    public static readonly ListRefundsRequestStatusItem Returned = new(Values.Returned);

    public ListRefundsRequestStatusItem(string value)
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
    public static ListRefundsRequestStatusItem FromCustom(string value)
    {
        return new ListRefundsRequestStatusItem(value);
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

    public static bool operator ==(ListRefundsRequestStatusItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListRefundsRequestStatusItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListRefundsRequestStatusItem value) => value.Value;

    public static explicit operator ListRefundsRequestStatusItem(string value) => new(value);

    internal class ListRefundsRequestStatusItemSerializer
        : JsonConverter<ListRefundsRequestStatusItem>
    {
        public override ListRefundsRequestStatusItem Read(
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
            return new ListRefundsRequestStatusItem(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ListRefundsRequestStatusItem value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ListRefundsRequestStatusItem ReadAsPropertyName(
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
            return new ListRefundsRequestStatusItem(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ListRefundsRequestStatusItem value,
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
        public const string Ready = "ready";

        public const string Pending = "pending";

        public const string Declined = "declined";

        public const string Complete = "complete";

        public const string Admin = "admin";

        public const string Reversal = "reversal";

        public const string Returned = "returned";
    }
}
