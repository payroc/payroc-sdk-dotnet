using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(TipMode.TipModeSerializer))]
[Serializable]
public readonly record struct TipMode : IStringEnum
{
    public static readonly TipMode Prompted = new(Values.Prompted);

    public static readonly TipMode Adjusted = new(Values.Adjusted);

    public TipMode(string value)
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
    public static TipMode FromCustom(string value)
    {
        return new TipMode(value);
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

    public static bool operator ==(TipMode value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(TipMode value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(TipMode value) => value.Value;

    public static explicit operator TipMode(string value) => new(value);

    internal class TipModeSerializer : JsonConverter<TipMode>
    {
        public override TipMode Read(
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
            return new TipMode(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TipMode value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TipMode ReadAsPropertyName(
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
            return new TipMode(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TipMode value,
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
        public const string Prompted = "prompted";

        public const string Adjusted = "adjusted";
    }
}
