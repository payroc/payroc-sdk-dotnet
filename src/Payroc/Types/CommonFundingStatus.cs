using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(CommonFundingStatus.CommonFundingStatusSerializer))]
[Serializable]
public readonly record struct CommonFundingStatus : IStringEnum
{
    public static readonly CommonFundingStatus Enabled = new(Values.Enabled);

    public static readonly CommonFundingStatus Disabled = new(Values.Disabled);

    public CommonFundingStatus(string value)
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
    public static CommonFundingStatus FromCustom(string value)
    {
        return new CommonFundingStatus(value);
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

    public static bool operator ==(CommonFundingStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CommonFundingStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CommonFundingStatus value) => value.Value;

    public static explicit operator CommonFundingStatus(string value) => new(value);

    internal class CommonFundingStatusSerializer : JsonConverter<CommonFundingStatus>
    {
        public override CommonFundingStatus Read(
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
            return new CommonFundingStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CommonFundingStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CommonFundingStatus ReadAsPropertyName(
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
            return new CommonFundingStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CommonFundingStatus value,
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
        public const string Enabled = "enabled";

        public const string Disabled = "disabled";
    }
}
