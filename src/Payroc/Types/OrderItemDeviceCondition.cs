using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<OrderItemDeviceCondition>))]
public readonly record struct OrderItemDeviceCondition : IStringEnum
{
    public static readonly OrderItemDeviceCondition New = new(Values.New);

    public static readonly OrderItemDeviceCondition Refurbished = new(Values.Refurbished);

    public OrderItemDeviceCondition(string value)
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
    public static OrderItemDeviceCondition FromCustom(string value)
    {
        return new OrderItemDeviceCondition(value);
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

    public static bool operator ==(OrderItemDeviceCondition value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OrderItemDeviceCondition value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OrderItemDeviceCondition value) => value.Value;

    public static explicit operator OrderItemDeviceCondition(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string New = "new";

        public const string Refurbished = "refurbished";
    }
}
