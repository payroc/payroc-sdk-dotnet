using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(DigitalWalletPayloadServiceProvider.DigitalWalletPayloadServiceProviderSerializer)
)]
[Serializable]
public readonly record struct DigitalWalletPayloadServiceProvider : IStringEnum
{
    public static readonly DigitalWalletPayloadServiceProvider Apple = new(Values.Apple);

    public static readonly DigitalWalletPayloadServiceProvider Google = new(Values.Google);

    public DigitalWalletPayloadServiceProvider(string value)
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
    public static DigitalWalletPayloadServiceProvider FromCustom(string value)
    {
        return new DigitalWalletPayloadServiceProvider(value);
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

    public static bool operator ==(DigitalWalletPayloadServiceProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DigitalWalletPayloadServiceProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DigitalWalletPayloadServiceProvider value) =>
        value.Value;

    public static explicit operator DigitalWalletPayloadServiceProvider(string value) => new(value);

    internal class DigitalWalletPayloadServiceProviderSerializer
        : JsonConverter<DigitalWalletPayloadServiceProvider>
    {
        public override DigitalWalletPayloadServiceProvider Read(
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
            return new DigitalWalletPayloadServiceProvider(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DigitalWalletPayloadServiceProvider value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override DigitalWalletPayloadServiceProvider ReadAsPropertyName(
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
            return new DigitalWalletPayloadServiceProvider(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            DigitalWalletPayloadServiceProvider value,
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
        public const string Apple = "apple";

        public const string Google = "google";
    }
}
