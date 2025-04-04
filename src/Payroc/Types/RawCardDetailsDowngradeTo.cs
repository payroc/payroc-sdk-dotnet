using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RawCardDetailsDowngradeTo>))]
public readonly record struct RawCardDetailsDowngradeTo : IStringEnum
{
    public static readonly RawCardDetailsDowngradeTo Keyed = Custom(Values.Keyed);

    public static readonly RawCardDetailsDowngradeTo Swiped = Custom(Values.Swiped);

    public RawCardDetailsDowngradeTo(string value)
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
    public static RawCardDetailsDowngradeTo Custom(string value)
    {
        return new RawCardDetailsDowngradeTo(value);
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

    public static bool operator ==(RawCardDetailsDowngradeTo value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RawCardDetailsDowngradeTo value1, string value2) =>
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
