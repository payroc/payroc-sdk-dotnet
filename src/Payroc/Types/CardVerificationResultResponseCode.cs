using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardVerificationResultResponseCode>))]
public readonly record struct CardVerificationResultResponseCode : IStringEnum
{
    public static readonly CardVerificationResultResponseCode A = Custom(Values.A);

    public static readonly CardVerificationResultResponseCode D = Custom(Values.D);

    public static readonly CardVerificationResultResponseCode E = Custom(Values.E);

    public static readonly CardVerificationResultResponseCode P = Custom(Values.P);

    public static readonly CardVerificationResultResponseCode R = Custom(Values.R);

    public static readonly CardVerificationResultResponseCode C = Custom(Values.C);

    public CardVerificationResultResponseCode(string value)
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
    public static CardVerificationResultResponseCode Custom(string value)
    {
        return new CardVerificationResultResponseCode(value);
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

    public static bool operator ==(CardVerificationResultResponseCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CardVerificationResultResponseCode value1, string value2) =>
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
