using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CustomizationOptionsEntryMethod>))]
public readonly record struct CustomizationOptionsEntryMethod : IStringEnum
{
    public static readonly CustomizationOptionsEntryMethod DeviceRead = new(Values.DeviceRead);

    public static readonly CustomizationOptionsEntryMethod ManualEntry = new(Values.ManualEntry);

    public CustomizationOptionsEntryMethod(string value)
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
    public static CustomizationOptionsEntryMethod FromCustom(string value)
    {
        return new CustomizationOptionsEntryMethod(value);
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

    public static bool operator ==(CustomizationOptionsEntryMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CustomizationOptionsEntryMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CustomizationOptionsEntryMethod value) => value.Value;

    public static explicit operator CustomizationOptionsEntryMethod(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string DeviceRead = "deviceRead";

        public const string ManualEntry = "manualEntry";
    }
}
