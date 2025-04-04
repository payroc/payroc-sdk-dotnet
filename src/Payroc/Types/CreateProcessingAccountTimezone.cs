using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CreateProcessingAccountTimezone>))]
public readonly record struct CreateProcessingAccountTimezone : IStringEnum
{
    public static readonly CreateProcessingAccountTimezone PacificMidway = Custom(
        Values.PacificMidway
    );

    public static readonly CreateProcessingAccountTimezone PacificHonolulu = Custom(
        Values.PacificHonolulu
    );

    public static readonly CreateProcessingAccountTimezone AmericaAnchorage = Custom(
        Values.AmericaAnchorage
    );

    public static readonly CreateProcessingAccountTimezone AmericaLosAngeles = Custom(
        Values.AmericaLosAngeles
    );

    public static readonly CreateProcessingAccountTimezone AmericaDenver = Custom(
        Values.AmericaDenver
    );

    public static readonly CreateProcessingAccountTimezone AmericaPhoenix = Custom(
        Values.AmericaPhoenix
    );

    public static readonly CreateProcessingAccountTimezone AmericaChicago = Custom(
        Values.AmericaChicago
    );

    public static readonly CreateProcessingAccountTimezone AmericaIndianaIndianapolis = Custom(
        Values.AmericaIndianaIndianapolis
    );

    public static readonly CreateProcessingAccountTimezone AmericaNewYork = Custom(
        Values.AmericaNewYork
    );

    public CreateProcessingAccountTimezone(string value)
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
    public static CreateProcessingAccountTimezone Custom(string value)
    {
        return new CreateProcessingAccountTimezone(value);
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

    public static bool operator ==(CreateProcessingAccountTimezone value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateProcessingAccountTimezone value1, string value2) =>
        !value1.Value.Equals(value2);

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
