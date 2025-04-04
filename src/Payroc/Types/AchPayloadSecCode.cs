using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<AchPayloadSecCode>))]
public readonly record struct AchPayloadSecCode : IStringEnum
{
    public static readonly AchPayloadSecCode Web = Custom(Values.Web);

    public static readonly AchPayloadSecCode Tel = Custom(Values.Tel);

    public static readonly AchPayloadSecCode Ccd = Custom(Values.Ccd);

    public static readonly AchPayloadSecCode Ppd = Custom(Values.Ppd);

    public AchPayloadSecCode(string value)
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
    public static AchPayloadSecCode Custom(string value)
    {
        return new AchPayloadSecCode(value);
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

    public static bool operator ==(AchPayloadSecCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AchPayloadSecCode value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Web = "web";

        public const string Tel = "tel";

        public const string Ccd = "ccd";

        public const string Ppd = "ppd";
    }
}
