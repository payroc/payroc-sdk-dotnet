using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PlainTextSwipedDataFormatFallbackReason>))]
[Serializable]
public readonly record struct PlainTextSwipedDataFormatFallbackReason : IStringEnum
{
    public static readonly PlainTextSwipedDataFormatFallbackReason Technical = new(
        Values.Technical
    );

    public static readonly PlainTextSwipedDataFormatFallbackReason RepeatFallback = new(
        Values.RepeatFallback
    );

    public static readonly PlainTextSwipedDataFormatFallbackReason EmptyCandidateList = new(
        Values.EmptyCandidateList
    );

    public PlainTextSwipedDataFormatFallbackReason(string value)
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
    public static PlainTextSwipedDataFormatFallbackReason FromCustom(string value)
    {
        return new PlainTextSwipedDataFormatFallbackReason(value);
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

    public static bool operator ==(PlainTextSwipedDataFormatFallbackReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlainTextSwipedDataFormatFallbackReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlainTextSwipedDataFormatFallbackReason value) =>
        value.Value;

    public static explicit operator PlainTextSwipedDataFormatFallbackReason(string value) =>
        new(value);

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
