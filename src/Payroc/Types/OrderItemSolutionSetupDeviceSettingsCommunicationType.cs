using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(OrderItemSolutionSetupDeviceSettingsCommunicationType.OrderItemSolutionSetupDeviceSettingsCommunicationTypeSerializer)
)]
[Serializable]
public readonly record struct OrderItemSolutionSetupDeviceSettingsCommunicationType : IStringEnum
{
    public static readonly OrderItemSolutionSetupDeviceSettingsCommunicationType Bluetooth = new(
        Values.Bluetooth
    );

    public static readonly OrderItemSolutionSetupDeviceSettingsCommunicationType Cellular = new(
        Values.Cellular
    );

    public static readonly OrderItemSolutionSetupDeviceSettingsCommunicationType Ethernet = new(
        Values.Ethernet
    );

    public static readonly OrderItemSolutionSetupDeviceSettingsCommunicationType Wifi = new(
        Values.Wifi
    );

    public OrderItemSolutionSetupDeviceSettingsCommunicationType(string value)
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
    public static OrderItemSolutionSetupDeviceSettingsCommunicationType FromCustom(string value)
    {
        return new OrderItemSolutionSetupDeviceSettingsCommunicationType(value);
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

    public static bool operator ==(
        OrderItemSolutionSetupDeviceSettingsCommunicationType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        OrderItemSolutionSetupDeviceSettingsCommunicationType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        OrderItemSolutionSetupDeviceSettingsCommunicationType value
    ) => value.Value;

    public static explicit operator OrderItemSolutionSetupDeviceSettingsCommunicationType(
        string value
    ) => new(value);

    internal class OrderItemSolutionSetupDeviceSettingsCommunicationTypeSerializer
        : JsonConverter<OrderItemSolutionSetupDeviceSettingsCommunicationType>
    {
        public override OrderItemSolutionSetupDeviceSettingsCommunicationType Read(
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
            return new OrderItemSolutionSetupDeviceSettingsCommunicationType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OrderItemSolutionSetupDeviceSettingsCommunicationType value,
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
        public const string Bluetooth = "bluetooth";

        public const string Cellular = "cellular";

        public const string Ethernet = "ethernet";

        public const string Wifi = "wifi";
    }
}
