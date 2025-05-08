using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<AchBankAccountSecCode>))]
public readonly record struct AchBankAccountSecCode : IStringEnum
{
    public static readonly AchBankAccountSecCode Web = new(Values.Web);

    public static readonly AchBankAccountSecCode Tel = new(Values.Tel);

    public static readonly AchBankAccountSecCode Ccd = new(Values.Ccd);

    public static readonly AchBankAccountSecCode Ppd = new(Values.Ppd);

    public AchBankAccountSecCode(string value)
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
    public static AchBankAccountSecCode FromCustom(string value)
    {
        return new AchBankAccountSecCode(value);
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

    public static bool operator ==(AchBankAccountSecCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AchBankAccountSecCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AchBankAccountSecCode value) => value.Value;

    public static explicit operator AchBankAccountSecCode(string value) => new(value);

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
