using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(RewardPayFeesTips.RewardPayFeesTipsSerializer))]
[Serializable]
public readonly record struct RewardPayFeesTips : IStringEnum
{
    public static readonly RewardPayFeesTips NoTips = new(Values.NoTips);

    public static readonly RewardPayFeesTips TipPrompt = new(Values.TipPrompt);

    public static readonly RewardPayFeesTips TipAdjust = new(Values.TipAdjust);

    public RewardPayFeesTips(string value)
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
    public static RewardPayFeesTips FromCustom(string value)
    {
        return new RewardPayFeesTips(value);
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

    public static bool operator ==(RewardPayFeesTips value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RewardPayFeesTips value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RewardPayFeesTips value) => value.Value;

    public static explicit operator RewardPayFeesTips(string value) => new(value);

    internal class RewardPayFeesTipsSerializer : JsonConverter<RewardPayFeesTips>
    {
        public override RewardPayFeesTips Read(
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
            return new RewardPayFeesTips(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RewardPayFeesTips value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RewardPayFeesTips ReadAsPropertyName(
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
            return new RewardPayFeesTips(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RewardPayFeesTips value,
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
