using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(DeviceCategory.DeviceCategorySerializer))]
[Serializable]
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

    internal class DeviceCategorySerializer : JsonConverter<DeviceCategory>
    {
        public override DeviceCategory Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new DeviceCategory(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DeviceCategory value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Attended = "attended";

        public const string Unattended = "unattended";
    }
}
