using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SecurityCheckCvvResult>))]
[Serializable]
public readonly record struct SecurityCheckCvvResult : IStringEnum
{
    public static readonly SecurityCheckCvvResult M = new(Values.M);

    public static readonly SecurityCheckCvvResult N = new(Values.N);

    public static readonly SecurityCheckCvvResult P = new(Values.P);

    public static readonly SecurityCheckCvvResult U = new(Values.U);

    public SecurityCheckCvvResult(string value)
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
    public static SecurityCheckCvvResult FromCustom(string value)
    {
        return new SecurityCheckCvvResult(value);
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

    public static bool operator ==(SecurityCheckCvvResult value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecurityCheckCvvResult value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SecurityCheckCvvResult value) => value.Value;

    public static explicit operator SecurityCheckCvvResult(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string M = "M";

        public const string N = "N";

        public const string P = "P";

        public const string U = "U";
    }
}
