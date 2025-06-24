using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SecurityCheckAvsResult>))]
[Serializable]
public readonly record struct SecurityCheckAvsResult : IStringEnum
{
    public static readonly SecurityCheckAvsResult Y = new(Values.Y);

    public static readonly SecurityCheckAvsResult A = new(Values.A);

    public static readonly SecurityCheckAvsResult Z = new(Values.Z);

    public static readonly SecurityCheckAvsResult N = new(Values.N);

    public static readonly SecurityCheckAvsResult U = new(Values.U);

    public static readonly SecurityCheckAvsResult R = new(Values.R);

    public static readonly SecurityCheckAvsResult G = new(Values.G);

    public static readonly SecurityCheckAvsResult S = new(Values.S);

    public static readonly SecurityCheckAvsResult F = new(Values.F);

    public static readonly SecurityCheckAvsResult W = new(Values.W);

    public static readonly SecurityCheckAvsResult X = new(Values.X);

    public SecurityCheckAvsResult(string value)
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
    public static SecurityCheckAvsResult FromCustom(string value)
    {
        return new SecurityCheckAvsResult(value);
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

    public static bool operator ==(SecurityCheckAvsResult value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecurityCheckAvsResult value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SecurityCheckAvsResult value) => value.Value;

    public static explicit operator SecurityCheckAvsResult(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Y = "Y";

        public const string A = "A";

        public const string Z = "Z";

        public const string N = "N";

        public const string U = "U";

        public const string R = "R";

        public const string G = "G";

        public const string S = "S";

        public const string F = "F";

        public const string W = "W";

        public const string X = "X";
    }
}
