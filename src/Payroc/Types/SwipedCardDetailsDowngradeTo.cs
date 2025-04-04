using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SwipedCardDetailsDowngradeTo>))]
public readonly record struct SwipedCardDetailsDowngradeTo : IStringEnum
{
    public static readonly SwipedCardDetailsDowngradeTo Keyed = Custom(Values.Keyed);

    public static readonly SwipedCardDetailsDowngradeTo Swiped = Custom(Values.Swiped);

    public SwipedCardDetailsDowngradeTo(string value)
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
    public static SwipedCardDetailsDowngradeTo Custom(string value)
    {
        return new SwipedCardDetailsDowngradeTo(value);
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

    public static bool operator ==(SwipedCardDetailsDowngradeTo value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SwipedCardDetailsDowngradeTo value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Keyed = "keyed";

        public const string Swiped = "swiped";
    }
}
