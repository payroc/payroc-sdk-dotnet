using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[JsonConverter(
    typeof(CreateTerminalOrderShippingPreferencesMethod.CreateTerminalOrderShippingPreferencesMethodSerializer)
)]
[Serializable]
public readonly record struct CreateTerminalOrderShippingPreferencesMethod : IStringEnum
{
    public static readonly CreateTerminalOrderShippingPreferencesMethod NextDay = new(
        Values.NextDay
    );

    public static readonly CreateTerminalOrderShippingPreferencesMethod Ground = new(Values.Ground);

    public CreateTerminalOrderShippingPreferencesMethod(string value)
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
    public static CreateTerminalOrderShippingPreferencesMethod FromCustom(string value)
    {
        return new CreateTerminalOrderShippingPreferencesMethod(value);
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
        CreateTerminalOrderShippingPreferencesMethod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateTerminalOrderShippingPreferencesMethod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateTerminalOrderShippingPreferencesMethod value) =>
        value.Value;

    public static explicit operator CreateTerminalOrderShippingPreferencesMethod(string value) =>
        new(value);

    internal class CreateTerminalOrderShippingPreferencesMethodSerializer
        : JsonConverter<CreateTerminalOrderShippingPreferencesMethod>
    {
        public override CreateTerminalOrderShippingPreferencesMethod Read(
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
            return new CreateTerminalOrderShippingPreferencesMethod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateTerminalOrderShippingPreferencesMethod value,
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
        public const string NextDay = "nextDay";

        public const string Ground = "ground";
    }
}
