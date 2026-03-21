using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(RewardPayChoiceFeesCreditTips.RewardPayChoiceFeesCreditTipsSerializer))]
[Serializable]
public readonly record struct RewardPayChoiceFeesCreditTips : IStringEnum
{
    public static readonly RewardPayChoiceFeesCreditTips NoTips = new(Values.NoTips);

    public static readonly RewardPayChoiceFeesCreditTips TipPrompt = new(Values.TipPrompt);

    public static readonly RewardPayChoiceFeesCreditTips TipAdjust = new(Values.TipAdjust);

    public RewardPayChoiceFeesCreditTips(string value)
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
    public static RewardPayChoiceFeesCreditTips FromCustom(string value)
    {
        return new RewardPayChoiceFeesCreditTips(value);
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

    public static bool operator ==(RewardPayChoiceFeesCreditTips value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RewardPayChoiceFeesCreditTips value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RewardPayChoiceFeesCreditTips value) => value.Value;

    public static explicit operator RewardPayChoiceFeesCreditTips(string value) => new(value);

    internal class RewardPayChoiceFeesCreditTipsSerializer
        : JsonConverter<RewardPayChoiceFeesCreditTips>
    {
        public override RewardPayChoiceFeesCreditTips Read(
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
            return new RewardPayChoiceFeesCreditTips(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RewardPayChoiceFeesCreditTips value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RewardPayChoiceFeesCreditTips ReadAsPropertyName(
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
            return new RewardPayChoiceFeesCreditTips(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RewardPayChoiceFeesCreditTips value,
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
        public const string NoTips = "noTips";

        public const string TipPrompt = "tipPrompt";

        public const string TipAdjust = "tipAdjust";
    }
}
