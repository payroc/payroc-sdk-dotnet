using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(BaseIntentStatus.BaseIntentStatusSerializer))]
[Serializable]
public readonly record struct BaseIntentStatus : IStringEnum
{
    public static readonly BaseIntentStatus Active = new(Values.Active);

    public static readonly BaseIntentStatus PendingReview = new(Values.PendingReview);

    public static readonly BaseIntentStatus Rejected = new(Values.Rejected);

    public BaseIntentStatus(string value)
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
    public static BaseIntentStatus FromCustom(string value)
    {
        return new BaseIntentStatus(value);
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

    public static bool operator ==(BaseIntentStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BaseIntentStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BaseIntentStatus value) => value.Value;

    public static explicit operator BaseIntentStatus(string value) => new(value);

    internal class BaseIntentStatusSerializer : JsonConverter<BaseIntentStatus>
    {
        public override BaseIntentStatus Read(
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
            return new BaseIntentStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BaseIntentStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override BaseIntentStatus ReadAsPropertyName(
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
            return new BaseIntentStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            BaseIntentStatus value,
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
        public const string Active = "active";

        public const string PendingReview = "pendingReview";

        public const string Rejected = "rejected";
    }
}
