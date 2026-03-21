using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(BaseUsAnnualFeeBillInMonth.BaseUsAnnualFeeBillInMonthSerializer))]
[Serializable]
public readonly record struct BaseUsAnnualFeeBillInMonth : IStringEnum
{
    public static readonly BaseUsAnnualFeeBillInMonth June = new(Values.June);

    public static readonly BaseUsAnnualFeeBillInMonth December = new(Values.December);

    public BaseUsAnnualFeeBillInMonth(string value)
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
    public static BaseUsAnnualFeeBillInMonth FromCustom(string value)
    {
        return new BaseUsAnnualFeeBillInMonth(value);
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

    public static bool operator ==(BaseUsAnnualFeeBillInMonth value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BaseUsAnnualFeeBillInMonth value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BaseUsAnnualFeeBillInMonth value) => value.Value;

    public static explicit operator BaseUsAnnualFeeBillInMonth(string value) => new(value);

    internal class BaseUsAnnualFeeBillInMonthSerializer : JsonConverter<BaseUsAnnualFeeBillInMonth>
    {
        public override BaseUsAnnualFeeBillInMonth Read(
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
            return new BaseUsAnnualFeeBillInMonth(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BaseUsAnnualFeeBillInMonth value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BaseUsAnnualFeeBillInMonth ReadAsPropertyName(
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
            return new BaseUsAnnualFeeBillInMonth(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BaseUsAnnualFeeBillInMonth value,
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
        public const string June = "june";

        public const string December = "december";
    }
}
