using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<OrderItemSolutionSetupDeviceSettingsCommunicationType>))]
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Bluetooth = "bluetooth";

        public const string Cellular = "cellular";

        public const string Ethernet = "ethernet";

        public const string Wifi = "wifi";
    }
}
