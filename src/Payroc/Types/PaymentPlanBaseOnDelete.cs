using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(PaymentPlanBaseOnDelete.PaymentPlanBaseOnDeleteSerializer))]
[Serializable]
public readonly record struct PaymentPlanBaseOnDelete : IStringEnum
{
    public static readonly PaymentPlanBaseOnDelete Complete = new(Values.Complete);

    public static readonly PaymentPlanBaseOnDelete Continue = new(Values.Continue);

    public PaymentPlanBaseOnDelete(string value)
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
    public static PaymentPlanBaseOnDelete FromCustom(string value)
    {
        return new PaymentPlanBaseOnDelete(value);
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

    public static bool operator ==(PaymentPlanBaseOnDelete value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanBaseOnDelete value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanBaseOnDelete value) => value.Value;

    public static explicit operator PaymentPlanBaseOnDelete(string value) => new(value);

    internal class PaymentPlanBaseOnDeleteSerializer : JsonConverter<PaymentPlanBaseOnDelete>
    {
        public override PaymentPlanBaseOnDelete Read(
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
            return new PaymentPlanBaseOnDelete(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PaymentPlanBaseOnDelete value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PaymentPlanBaseOnDelete ReadAsPropertyName(
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
            return new PaymentPlanBaseOnDelete(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PaymentPlanBaseOnDelete value,
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
        public const string Complete = "complete";

        public const string Continue = "continue";
    }
}
