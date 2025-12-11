using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RetrievedCardEntryMethod>))]
[Serializable]
public readonly record struct RetrievedCardEntryMethod : IStringEnum
{
    public static readonly RetrievedCardEntryMethod Icc = new(Values.Icc);

    public static readonly RetrievedCardEntryMethod Keyed = new(Values.Keyed);

    public static readonly RetrievedCardEntryMethod Swiped = new(Values.Swiped);

    public static readonly RetrievedCardEntryMethod SwipedFallback = new(Values.SwipedFallback);

    public static readonly RetrievedCardEntryMethod ContactlessIcc = new(Values.ContactlessIcc);

    public static readonly RetrievedCardEntryMethod ContactlessMsr = new(Values.ContactlessMsr);

    public RetrievedCardEntryMethod(string value)
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
    public static RetrievedCardEntryMethod FromCustom(string value)
    {
        return new RetrievedCardEntryMethod(value);
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

    public static bool operator ==(RetrievedCardEntryMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RetrievedCardEntryMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RetrievedCardEntryMethod value) => value.Value;

    public static explicit operator RetrievedCardEntryMethod(string value) => new(value);

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
