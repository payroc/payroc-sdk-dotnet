using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Refunds;

[JsonConverter(typeof(ListRefundsRequestTender.ListRefundsRequestTenderSerializer))]
[Serializable]
public readonly record struct ListRefundsRequestTender : IStringEnum
{
    public static readonly ListRefundsRequestTender Ebt = new(Values.Ebt);

    public static readonly ListRefundsRequestTender CreditDebit = new(Values.CreditDebit);

    public ListRefundsRequestTender(string value)
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
    public static ListRefundsRequestTender FromCustom(string value)
    {
        return new ListRefundsRequestTender(value);
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

    public static bool operator ==(ListRefundsRequestTender value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListRefundsRequestTender value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListRefundsRequestTender value) => value.Value;

    public static explicit operator ListRefundsRequestTender(string value) => new(value);

    internal class ListRefundsRequestTenderSerializer : JsonConverter<ListRefundsRequestTender>
    {
        public override ListRefundsRequestTender Read(
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
            return new ListRefundsRequestTender(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ListRefundsRequestTender value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Ebt = "ebt";

        public const string CreditDebit = "creditDebit";
    }
}
