using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CommunicationType>))]
[Serializable]
public readonly record struct CommunicationType : IStringEnum
{
    public static readonly CommunicationType Bluetooth = new(Values.Bluetooth);

    public static readonly CommunicationType Cellular = new(Values.Cellular);

    public static readonly CommunicationType Ethernet = new(Values.Ethernet);

    public static readonly CommunicationType Wifi = new(Values.Wifi);

    public CommunicationType(string value)
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
    public static CommunicationType FromCustom(string value)
    {
        return new CommunicationType(value);
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

    public static bool operator ==(CommunicationType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CommunicationType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CommunicationType value) => value.Value;

    public static explicit operator CommunicationType(string value) => new(value);

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
