using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PayrocGatewayGateway>))]
[Serializable]
public readonly record struct PayrocGatewayGateway : IStringEnum
{
    public static readonly PayrocGatewayGateway Payroc = new(Values.Payroc);

    public PayrocGatewayGateway(string value)
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
    public static PayrocGatewayGateway FromCustom(string value)
    {
        return new PayrocGatewayGateway(value);
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

    public static bool operator ==(PayrocGatewayGateway value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PayrocGatewayGateway value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PayrocGatewayGateway value) => value.Value;

    public static explicit operator PayrocGatewayGateway(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Payroc = "payroc";
    }
}
