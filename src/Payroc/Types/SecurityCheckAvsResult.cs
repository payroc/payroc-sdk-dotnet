using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SecurityCheckAvsResult>))]
public readonly record struct SecurityCheckAvsResult : IStringEnum
{
    public static readonly SecurityCheckAvsResult Y = Custom(Values.Y);

    public static readonly SecurityCheckAvsResult A = Custom(Values.A);

    public static readonly SecurityCheckAvsResult Z = Custom(Values.Z);

    public static readonly SecurityCheckAvsResult N = Custom(Values.N);

    public static readonly SecurityCheckAvsResult U = Custom(Values.U);

    public static readonly SecurityCheckAvsResult R = Custom(Values.R);

    public static readonly SecurityCheckAvsResult G = Custom(Values.G);

    public static readonly SecurityCheckAvsResult S = Custom(Values.S);

    public static readonly SecurityCheckAvsResult F = Custom(Values.F);

    public static readonly SecurityCheckAvsResult W = Custom(Values.W);

    public static readonly SecurityCheckAvsResult X = Custom(Values.X);

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
    public static SecurityCheckAvsResult Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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
