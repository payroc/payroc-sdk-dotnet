using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingAccountTimezone>))]
public readonly record struct ProcessingAccountTimezone : IStringEnum
{
    public static readonly ProcessingAccountTimezone PacificMidway = new(Values.PacificMidway);

    public static readonly ProcessingAccountTimezone PacificHonolulu = new(Values.PacificHonolulu);

    public static readonly ProcessingAccountTimezone AmericaAnchorage = new(
        Values.AmericaAnchorage
    );

    public static readonly ProcessingAccountTimezone AmericaLosAngeles = new(
        Values.AmericaLosAngeles
    );

    public static readonly ProcessingAccountTimezone AmericaDenver = new(Values.AmericaDenver);

    public static readonly ProcessingAccountTimezone AmericaPhoenix = new(Values.AmericaPhoenix);

    public static readonly ProcessingAccountTimezone AmericaChicago = new(Values.AmericaChicago);

    public static readonly ProcessingAccountTimezone AmericaIndianaIndianapolis = new(
        Values.AmericaIndianaIndianapolis
    );

    public static readonly ProcessingAccountTimezone AmericaNewYork = new(Values.AmericaNewYork);

    public ProcessingAccountTimezone(string value)
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
    public static ProcessingAccountTimezone FromCustom(string value)
    {
        return new ProcessingAccountTimezone(value);
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

    public static bool operator ==(ProcessingAccountTimezone value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingAccountTimezone value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingAccountTimezone value) => value.Value;

    public static explicit operator ProcessingAccountTimezone(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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
