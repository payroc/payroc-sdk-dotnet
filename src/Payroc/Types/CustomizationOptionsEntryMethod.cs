using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(CustomizationOptionsEntryMethod.CustomizationOptionsEntryMethodSerializer))]
[Serializable]
public readonly record struct CustomizationOptionsEntryMethod : IStringEnum
{
    public static readonly CustomizationOptionsEntryMethod DeviceRead = new(Values.DeviceRead);

    public static readonly CustomizationOptionsEntryMethod ManualEntry = new(Values.ManualEntry);

    public static readonly CustomizationOptionsEntryMethod DeviceReadOrManualEntry = new(
        Values.DeviceReadOrManualEntry
    );

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

    internal class CustomizationOptionsEntryMethodSerializer
        : JsonConverter<CustomizationOptionsEntryMethod>
    {
        public override CustomizationOptionsEntryMethod Read(
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
            return new CustomizationOptionsEntryMethod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CustomizationOptionsEntryMethod value,
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
        public const string DeviceRead = "deviceRead";

        public const string ManualEntry = "manualEntry";

        public const string DeviceReadOrManualEntry = "deviceReadOrManualEntry";
    }
}
