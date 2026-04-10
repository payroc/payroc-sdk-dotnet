using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(EncryptedSwipedDataFormatFallbackReason.EncryptedSwipedDataFormatFallbackReasonSerializer)
)]
[Serializable]
public readonly record struct EncryptedSwipedDataFormatFallbackReason : IStringEnum
{
    public static readonly EncryptedSwipedDataFormatFallbackReason Technical = new(
        Values.Technical
    );

    public static readonly EncryptedSwipedDataFormatFallbackReason RepeatFallback = new(
        Values.RepeatFallback
    );

    public static readonly EncryptedSwipedDataFormatFallbackReason EmptyCandidateList = new(
        Values.EmptyCandidateList
    );

    public EncryptedSwipedDataFormatFallbackReason(string value)
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
    public static EncryptedSwipedDataFormatFallbackReason FromCustom(string value)
    {
        return new EncryptedSwipedDataFormatFallbackReason(value);
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

    public static bool operator ==(EncryptedSwipedDataFormatFallbackReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EncryptedSwipedDataFormatFallbackReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EncryptedSwipedDataFormatFallbackReason value) =>
        value.Value;

    public static explicit operator EncryptedSwipedDataFormatFallbackReason(string value) =>
        new(value);

    internal class EncryptedSwipedDataFormatFallbackReasonSerializer
        : JsonConverter<EncryptedSwipedDataFormatFallbackReason>
    {
        public override EncryptedSwipedDataFormatFallbackReason Read(
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
            return new EncryptedSwipedDataFormatFallbackReason(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            EncryptedSwipedDataFormatFallbackReason value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override EncryptedSwipedDataFormatFallbackReason ReadAsPropertyName(
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
            return new EncryptedSwipedDataFormatFallbackReason(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            EncryptedSwipedDataFormatFallbackReason value,
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
        public const string Technical = "technical";

        public const string RepeatFallback = "repeatFallback";

        public const string EmptyCandidateList = "emptyCandidateList";
    }
}
