using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<Timezone>))]
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
