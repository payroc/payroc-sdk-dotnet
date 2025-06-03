using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PropertiesShippingPreferencesMethod>))]
public readonly record struct PropertiesShippingPreferencesMethod : IStringEnum
{
    public static readonly PropertiesShippingPreferencesMethod NextDay = new(Values.NextDay);

    public static readonly PropertiesShippingPreferencesMethod Ground = new(Values.Ground);

    public PropertiesShippingPreferencesMethod(string value)
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
    public static PropertiesShippingPreferencesMethod FromCustom(string value)
    {
        return new PropertiesShippingPreferencesMethod(value);
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

    public static bool operator ==(PropertiesShippingPreferencesMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PropertiesShippingPreferencesMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PropertiesShippingPreferencesMethod value) =>
        value.Value;

    public static explicit operator PropertiesShippingPreferencesMethod(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string NextDay = "nextDay";

        public const string Ground = "ground";
    }
}
