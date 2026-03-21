using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StandingInstructionsSequence.StandingInstructionsSequenceSerializer))]
[Serializable]
public readonly record struct StandingInstructionsSequence : IStringEnum
{
    public static readonly StandingInstructionsSequence First = new(Values.First);

    public static readonly StandingInstructionsSequence Subsequent = new(Values.Subsequent);

    public StandingInstructionsSequence(string value)
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
    public static StandingInstructionsSequence FromCustom(string value)
    {
        return new StandingInstructionsSequence(value);
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

    public static bool operator ==(StandingInstructionsSequence value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StandingInstructionsSequence value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(StandingInstructionsSequence value) => value.Value;

    public static explicit operator StandingInstructionsSequence(string value) => new(value);

    internal class StandingInstructionsSequenceSerializer
        : JsonConverter<StandingInstructionsSequence>
    {
        public override StandingInstructionsSequence Read(
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
            return new StandingInstructionsSequence(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            StandingInstructionsSequence value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override StandingInstructionsSequence ReadAsPropertyName(
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
            return new StandingInstructionsSequence(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            StandingInstructionsSequence value,
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
        public const string First = "first";

        public const string Subsequent = "subsequent";
    }
}
