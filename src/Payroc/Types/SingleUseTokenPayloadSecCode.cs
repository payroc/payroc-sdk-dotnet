using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SingleUseTokenPayloadSecCode.SingleUseTokenPayloadSecCodeSerializer))]
[Serializable]
public readonly record struct SingleUseTokenPayloadSecCode : IStringEnum
{
    public static readonly SingleUseTokenPayloadSecCode Web = new(Values.Web);

    public static readonly SingleUseTokenPayloadSecCode Tel = new(Values.Tel);

    public static readonly SingleUseTokenPayloadSecCode Ccd = new(Values.Ccd);

    public static readonly SingleUseTokenPayloadSecCode Ppd = new(Values.Ppd);

    public SingleUseTokenPayloadSecCode(string value)
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
    public static SingleUseTokenPayloadSecCode FromCustom(string value)
    {
        return new SingleUseTokenPayloadSecCode(value);
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

    public static bool operator ==(SingleUseTokenPayloadSecCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUseTokenPayloadSecCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SingleUseTokenPayloadSecCode value) => value.Value;

    public static explicit operator SingleUseTokenPayloadSecCode(string value) => new(value);

    internal class SingleUseTokenPayloadSecCodeSerializer
        : JsonConverter<SingleUseTokenPayloadSecCode>
    {
        public override SingleUseTokenPayloadSecCode Read(
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
            return new SingleUseTokenPayloadSecCode(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SingleUseTokenPayloadSecCode value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SingleUseTokenPayloadSecCode ReadAsPropertyName(
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
            return new SingleUseTokenPayloadSecCode(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SingleUseTokenPayloadSecCode value,
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
        public const string Web = "web";

        public const string Tel = "tel";

        public const string Ccd = "ccd";

        public const string Ppd = "ppd";
    }
}
