using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<EncryptedSwipedDataFormatFallbackReason>))]
public readonly record struct EncryptedSwipedDataFormatFallbackReason : IStringEnum
{
    public static readonly EncryptedSwipedDataFormatFallbackReason Technical = Custom(
        Values.Technical
    );

    public static readonly EncryptedSwipedDataFormatFallbackReason RepeatFallback = Custom(
        Values.RepeatFallback
    );

    public static readonly EncryptedSwipedDataFormatFallbackReason EmptyCandidateList = Custom(
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
    public static EncryptedSwipedDataFormatFallbackReason Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Technical = "technical";

        public const string RepeatFallback = "repeatFallback";

        public const string EmptyCandidateList = "emptyCandidateList";
    }
}
