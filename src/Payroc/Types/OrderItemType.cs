using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(OrderItemType.OrderItemTypeSerializer))]
[Serializable]
public readonly record struct OrderItemType : IStringEnum
{
    public static readonly OrderItemType Solution = new(Values.Solution);

    public OrderItemType(string value)
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
    public static OrderItemType FromCustom(string value)
    {
        return new OrderItemType(value);
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

    public static bool operator ==(OrderItemType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(OrderItemType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(OrderItemType value) => value.Value;

    public static explicit operator OrderItemType(string value) => new(value);

    internal class OrderItemTypeSerializer : JsonConverter<OrderItemType>
    {
        public override OrderItemType Read(
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
            return new OrderItemType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            OrderItemType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override OrderItemType ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new OrderItemType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            OrderItemType value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Solution = "solution";
    }
}
