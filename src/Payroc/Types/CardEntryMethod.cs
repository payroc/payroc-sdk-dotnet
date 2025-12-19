using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardEntryMethod>))]
[Serializable]
public readonly record struct CardEntryMethod : IStringEnum
{
    public static readonly CardEntryMethod Icc = new(Values.Icc);

    public static readonly CardEntryMethod Keyed = new(Values.Keyed);

    public static readonly CardEntryMethod Swiped = new(Values.Swiped);

    public static readonly CardEntryMethod SwipedFallback = new(Values.SwipedFallback);

    public static readonly CardEntryMethod ContactlessIcc = new(Values.ContactlessIcc);

    public static readonly CardEntryMethod ContactlessMsr = new(Values.ContactlessMsr);

    public CardEntryMethod(string value)
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
    public static CardEntryMethod FromCustom(string value)
    {
        return new CardEntryMethod(value);
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

    public static bool operator ==(CardEntryMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CardEntryMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CardEntryMethod value) => value.Value;

    public static explicit operator CardEntryMethod(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Icc = "icc";

        public const string Keyed = "keyed";

        public const string Swiped = "swiped";

        public const string SwipedFallback = "swipedFallback";

        public const string ContactlessIcc = "contactlessIcc";

        public const string ContactlessMsr = "contactlessMsr";
    }
}
