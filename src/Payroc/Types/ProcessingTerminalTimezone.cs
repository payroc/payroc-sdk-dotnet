using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ProcessingTerminalTimezone.ProcessingTerminalTimezoneSerializer))]
[Serializable]
public readonly record struct ProcessingTerminalTimezone : IStringEnum
{
    public static readonly ProcessingTerminalTimezone PacificMidway = new(Values.PacificMidway);

    public static readonly ProcessingTerminalTimezone PacificHonolulu = new(Values.PacificHonolulu);

    public static readonly ProcessingTerminalTimezone AmericaAnchorage = new(
        Values.AmericaAnchorage
    );

    public static readonly ProcessingTerminalTimezone AmericaLosAngeles = new(
        Values.AmericaLosAngeles
    );

    public static readonly ProcessingTerminalTimezone AmericaDenver = new(Values.AmericaDenver);

    public static readonly ProcessingTerminalTimezone AmericaPhoenix = new(Values.AmericaPhoenix);

    public static readonly ProcessingTerminalTimezone AmericaChicago = new(Values.AmericaChicago);

    public static readonly ProcessingTerminalTimezone AmericaIndianaIndianapolis = new(
        Values.AmericaIndianaIndianapolis
    );

    public static readonly ProcessingTerminalTimezone AmericaNewYork = new(Values.AmericaNewYork);

    public ProcessingTerminalTimezone(string value)
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
    public static ProcessingTerminalTimezone FromCustom(string value)
    {
        return new ProcessingTerminalTimezone(value);
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

    public static bool operator ==(ProcessingTerminalTimezone value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingTerminalTimezone value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingTerminalTimezone value) => value.Value;

    public static explicit operator ProcessingTerminalTimezone(string value) => new(value);

    internal class ProcessingTerminalTimezoneSerializer : JsonConverter<ProcessingTerminalTimezone>
    {
        public override ProcessingTerminalTimezone Read(
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
            return new ProcessingTerminalTimezone(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProcessingTerminalTimezone value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ProcessingTerminalTimezone ReadAsPropertyName(
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
            return new ProcessingTerminalTimezone(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ProcessingTerminalTimezone value,
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
        public const string PacificMidway = "Pacific/Midway";

        public const string PacificHonolulu = "Pacific/Honolulu";

        public const string AmericaAnchorage = "America/Anchorage";

        public const string AmericaLosAngeles = "America/Los_Angeles";

        public const string AmericaDenver = "America/Denver";

        public const string AmericaPhoenix = "America/Phoenix";

        public const string AmericaChicago = "America/Chicago";

        public const string AmericaIndianaIndianapolis = "America/Indiana/Indianapolis";

        public const string AmericaNewYork = "America/New_York";
    }
}
