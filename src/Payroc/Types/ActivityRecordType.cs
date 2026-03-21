using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ActivityRecordType.ActivityRecordTypeSerializer))]
[Serializable]
public readonly record struct ActivityRecordType : IStringEnum
{
    public static readonly ActivityRecordType Credit = new(Values.Credit);

    public static readonly ActivityRecordType Debit = new(Values.Debit);

    public ActivityRecordType(string value)
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
    public static ActivityRecordType FromCustom(string value)
    {
        return new ActivityRecordType(value);
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

    public static bool operator ==(ActivityRecordType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ActivityRecordType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ActivityRecordType value) => value.Value;

    public static explicit operator ActivityRecordType(string value) => new(value);

    internal class ActivityRecordTypeSerializer : JsonConverter<ActivityRecordType>
    {
        public override ActivityRecordType Read(
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
            return new ActivityRecordType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ActivityRecordType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ActivityRecordType ReadAsPropertyName(
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
            return new ActivityRecordType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ActivityRecordType value,
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
        public const string Credit = "credit";

        public const string Debit = "debit";
    }
}
