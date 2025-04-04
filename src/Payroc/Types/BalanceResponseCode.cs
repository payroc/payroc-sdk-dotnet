using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BalanceResponseCode>))]
public readonly record struct BalanceResponseCode : IStringEnum
{
    public static readonly BalanceResponseCode A = Custom(Values.A);

    public static readonly BalanceResponseCode D = Custom(Values.D);

    public static readonly BalanceResponseCode E = Custom(Values.E);

    public static readonly BalanceResponseCode P = Custom(Values.P);

    public static readonly BalanceResponseCode R = Custom(Values.R);

    public static readonly BalanceResponseCode C = Custom(Values.C);

    public BalanceResponseCode(string value)
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
    public static BalanceResponseCode Custom(string value)
    {
        return new BalanceResponseCode(value);
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

    public static bool operator ==(BalanceResponseCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BalanceResponseCode value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string A = "A";

        public const string D = "D";

        public const string E = "E";

        public const string P = "P";

        public const string R = "R";

        public const string C = "C";
    }
}
