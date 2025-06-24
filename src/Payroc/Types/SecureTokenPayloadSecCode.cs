using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SecureTokenPayloadSecCode>))]
[Serializable]
public readonly record struct SecureTokenPayloadSecCode : IStringEnum
{
    public static readonly SecureTokenPayloadSecCode Web = new(Values.Web);

    public static readonly SecureTokenPayloadSecCode Tel = new(Values.Tel);

    public static readonly SecureTokenPayloadSecCode Ccd = new(Values.Ccd);

    public static readonly SecureTokenPayloadSecCode Ppd = new(Values.Ppd);

    public SecureTokenPayloadSecCode(string value)
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
    public static SecureTokenPayloadSecCode FromCustom(string value)
    {
        return new SecureTokenPayloadSecCode(value);
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

    public static bool operator ==(SecureTokenPayloadSecCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecureTokenPayloadSecCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SecureTokenPayloadSecCode value) => value.Value;

    public static explicit operator SecureTokenPayloadSecCode(string value) => new(value);

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
