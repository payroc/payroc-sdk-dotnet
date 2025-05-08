using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<IpAddressType>))]
public readonly record struct IpAddressType : IStringEnum
{
    public static readonly IpAddressType Ipv4 = new(Values.Ipv4);

    public static readonly IpAddressType Ipv6 = new(Values.Ipv6);

    public IpAddressType(string value)
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
    public static IpAddressType FromCustom(string value)
    {
        return new IpAddressType(value);
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

    public static bool operator ==(IpAddressType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(IpAddressType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(IpAddressType value) => value.Value;

    public static explicit operator IpAddressType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Ipv4 = "ipv4";

        public const string Ipv6 = "ipv6";
    }
}
