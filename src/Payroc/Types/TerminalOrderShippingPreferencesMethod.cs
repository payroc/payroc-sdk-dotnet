using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(TerminalOrderShippingPreferencesMethod.TerminalOrderShippingPreferencesMethodSerializer)
)]
[Serializable]
public readonly record struct TerminalOrderShippingPreferencesMethod : IStringEnum
{
    public static readonly TerminalOrderShippingPreferencesMethod NextDay = new(Values.NextDay);

    public static readonly TerminalOrderShippingPreferencesMethod Ground = new(Values.Ground);

    public TerminalOrderShippingPreferencesMethod(string value)
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
    public static TerminalOrderShippingPreferencesMethod FromCustom(string value)
    {
        return new TerminalOrderShippingPreferencesMethod(value);
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

    public static bool operator ==(TerminalOrderShippingPreferencesMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TerminalOrderShippingPreferencesMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TerminalOrderShippingPreferencesMethod value) =>
        value.Value;

    public static explicit operator TerminalOrderShippingPreferencesMethod(string value) =>
        new(value);

    internal class TerminalOrderShippingPreferencesMethodSerializer
        : JsonConverter<TerminalOrderShippingPreferencesMethod>
    {
        public override TerminalOrderShippingPreferencesMethod Read(
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
            return new TerminalOrderShippingPreferencesMethod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TerminalOrderShippingPreferencesMethod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TerminalOrderShippingPreferencesMethod ReadAsPropertyName(
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
            return new TerminalOrderShippingPreferencesMethod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TerminalOrderShippingPreferencesMethod value,
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
        public const string NextDay = "nextDay";

        public const string Ground = "ground";
    }
}
