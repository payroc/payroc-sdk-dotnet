using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DeviceCategory>))]
public readonly record struct DeviceCategory : IStringEnum
{
    public static readonly DeviceCategory Attended = new(Values.Attended);

    public static readonly DeviceCategory Unattended = new(Values.Unattended);

    public DeviceCategory(string value)
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
    public static DeviceCategory FromCustom(string value)
    {
        return new DeviceCategory(value);
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

    public static bool operator ==(DeviceCategory value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DeviceCategory value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DeviceCategory value) => value.Value;

    public static explicit operator DeviceCategory(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Attended = "attended";

        public const string Unattended = "unattended";
    }
}
