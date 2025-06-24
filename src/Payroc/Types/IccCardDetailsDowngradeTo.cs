using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<IccCardDetailsDowngradeTo>))]
[Serializable]
public readonly record struct IccCardDetailsDowngradeTo : IStringEnum
{
    public static readonly IccCardDetailsDowngradeTo Keyed = new(Values.Keyed);

    public static readonly IccCardDetailsDowngradeTo Swiped = new(Values.Swiped);

    public IccCardDetailsDowngradeTo(string value)
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
    public static IccCardDetailsDowngradeTo FromCustom(string value)
    {
        return new IccCardDetailsDowngradeTo(value);
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

    public static bool operator ==(IccCardDetailsDowngradeTo value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(IccCardDetailsDowngradeTo value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(IccCardDetailsDowngradeTo value) => value.Value;

    public static explicit operator IccCardDetailsDowngradeTo(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Keyed = "keyed";

        public const string Swiped = "swiped";
    }
}
