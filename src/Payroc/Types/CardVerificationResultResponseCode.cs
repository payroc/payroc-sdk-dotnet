using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardVerificationResultResponseCode>))]
[Serializable]
public readonly record struct CardVerificationResultResponseCode : IStringEnum
{
    public static readonly CardVerificationResultResponseCode A = new(Values.A);

    public static readonly CardVerificationResultResponseCode D = new(Values.D);

    public static readonly CardVerificationResultResponseCode E = new(Values.E);

    public static readonly CardVerificationResultResponseCode P = new(Values.P);

    public static readonly CardVerificationResultResponseCode R = new(Values.R);

    public static readonly CardVerificationResultResponseCode C = new(Values.C);

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
    public static CardVerificationResultResponseCode FromCustom(string value)
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

    public static explicit operator string(CardVerificationResultResponseCode value) => value.Value;

    public static explicit operator CardVerificationResultResponseCode(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
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
