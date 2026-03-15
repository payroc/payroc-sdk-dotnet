using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.CardPayments.Refunds;

[JsonConverter(typeof(UnreferencedRefundChannel.UnreferencedRefundChannelSerializer))]
[Serializable]
public readonly record struct UnreferencedRefundChannel : IStringEnum
{
    public static readonly UnreferencedRefundChannel Pos = new(Values.Pos);

    public static readonly UnreferencedRefundChannel Moto = new(Values.Moto);

    public UnreferencedRefundChannel(string value)
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
    public static UnreferencedRefundChannel FromCustom(string value)
    {
        return new UnreferencedRefundChannel(value);
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

    public static bool operator ==(UnreferencedRefundChannel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UnreferencedRefundChannel value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UnreferencedRefundChannel value) => value.Value;

    public static explicit operator UnreferencedRefundChannel(string value) => new(value);

    internal class UnreferencedRefundChannelSerializer : JsonConverter<UnreferencedRefundChannel>
    {
        public override UnreferencedRefundChannel Read(
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
            return new UnreferencedRefundChannel(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            UnreferencedRefundChannel value,
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
        public const string Pos = "pos";

        public const string Moto = "moto";
    }
}
