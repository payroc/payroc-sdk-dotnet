using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<EbtEnabledEbtType>))]
public readonly record struct EbtEnabledEbtType : IStringEnum
{
    public static readonly EbtEnabledEbtType FoodStamp = new(Values.FoodStamp);

    public static readonly EbtEnabledEbtType Cash = new(Values.Cash);

    public static readonly EbtEnabledEbtType Both = new(Values.Both);

    public EbtEnabledEbtType(string value)
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
    public static EbtEnabledEbtType FromCustom(string value)
    {
        return new EbtEnabledEbtType(value);
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

    public static bool operator ==(EbtEnabledEbtType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EbtEnabledEbtType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EbtEnabledEbtType value) => value.Value;

    public static explicit operator EbtEnabledEbtType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string FoodStamp = "foodStamp";

        public const string Cash = "cash";

        public const string Both = "both";
    }
}
