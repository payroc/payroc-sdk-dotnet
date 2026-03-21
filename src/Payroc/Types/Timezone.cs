using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(Timezone.TimezoneSerializer))]
[Serializable]
public readonly record struct Timezone : IStringEnum
{
    public static readonly Timezone PacificMidway = new(Values.PacificMidway);

    public static readonly Timezone PacificHonolulu = new(Values.PacificHonolulu);

    public static readonly Timezone AmericaAnchorage = new(Values.AmericaAnchorage);

    public static readonly Timezone AmericaLosAngeles = new(Values.AmericaLosAngeles);

    public static readonly Timezone AmericaDenver = new(Values.AmericaDenver);

    public static readonly Timezone AmericaPhoenix = new(Values.AmericaPhoenix);

    public static readonly Timezone AmericaChicago = new(Values.AmericaChicago);

    public static readonly Timezone AmericaIndianaIndianapolis = new(
        Values.AmericaIndianaIndianapolis
    );

    public static readonly Timezone AmericaNewYork = new(Values.AmericaNewYork);

    public Timezone(string value)
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
    public static Timezone FromCustom(string value)
    {
        return new Timezone(value);
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

    public static bool operator ==(Timezone value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(Timezone value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(Timezone value) => value.Value;

    public static explicit operator Timezone(string value) => new(value);

    internal class TimezoneSerializer : JsonConverter<Timezone>
    {
        public override Timezone Read(
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
            return new Timezone(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            Timezone value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override Timezone ReadAsPropertyName(
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
            return new Timezone(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            Timezone value,
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
