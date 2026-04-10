using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PaymentSummaryResponseCode.PaymentSummaryResponseCodeSerializer))]
[Serializable]
public readonly record struct PaymentSummaryResponseCode : IStringEnum
{
    public static readonly PaymentSummaryResponseCode A = new(Values.A);

    public static readonly PaymentSummaryResponseCode D = new(Values.D);

    public static readonly PaymentSummaryResponseCode E = new(Values.E);

    public static readonly PaymentSummaryResponseCode P = new(Values.P);

    public static readonly PaymentSummaryResponseCode R = new(Values.R);

    public static readonly PaymentSummaryResponseCode C = new(Values.C);

    public PaymentSummaryResponseCode(string value)
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
    public static PaymentSummaryResponseCode FromCustom(string value)
    {
        return new PaymentSummaryResponseCode(value);
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

    public static bool operator ==(PaymentSummaryResponseCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentSummaryResponseCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentSummaryResponseCode value) => value.Value;

    public static explicit operator PaymentSummaryResponseCode(string value) => new(value);

    internal class PaymentSummaryResponseCodeSerializer : JsonConverter<PaymentSummaryResponseCode>
    {
        public override PaymentSummaryResponseCode Read(
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
            return new PaymentSummaryResponseCode(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PaymentSummaryResponseCode value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PaymentSummaryResponseCode ReadAsPropertyName(
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
            return new PaymentSummaryResponseCode(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PaymentSummaryResponseCode value,
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
        public const string A = "A";

        public const string D = "D";

        public const string E = "E";

        public const string P = "P";

        public const string R = "R";

        public const string C = "C";
    }
}
