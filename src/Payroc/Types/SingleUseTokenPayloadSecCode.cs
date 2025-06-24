using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SingleUseTokenPayloadSecCode>))]
[Serializable]
public readonly record struct SingleUseTokenPayloadSecCode : IStringEnum
{
    public static readonly SingleUseTokenPayloadSecCode Web = new(Values.Web);

    public static readonly SingleUseTokenPayloadSecCode Tel = new(Values.Tel);

    public static readonly SingleUseTokenPayloadSecCode Ccd = new(Values.Ccd);

    public static readonly SingleUseTokenPayloadSecCode Ppd = new(Values.Ppd);

    public SingleUseTokenPayloadSecCode(string value)
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
    public static SingleUseTokenPayloadSecCode FromCustom(string value)
    {
        return new SingleUseTokenPayloadSecCode(value);
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

    public static bool operator ==(SingleUseTokenPayloadSecCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUseTokenPayloadSecCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SingleUseTokenPayloadSecCode value) => value.Value;

    public static explicit operator SingleUseTokenPayloadSecCode(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Web = "web";

        public const string Tel = "tel";

        public const string Ccd = "ccd";

        public const string Ppd = "ppd";
    }
}
