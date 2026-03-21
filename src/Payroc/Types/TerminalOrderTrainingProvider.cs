using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(TerminalOrderTrainingProvider.TerminalOrderTrainingProviderSerializer))]
[Serializable]
public readonly record struct TerminalOrderTrainingProvider : IStringEnum
{
    public static readonly TerminalOrderTrainingProvider Partner = new(Values.Partner);

    public static readonly TerminalOrderTrainingProvider Payroc = new(Values.Payroc);

    public TerminalOrderTrainingProvider(string value)
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
    public static TerminalOrderTrainingProvider FromCustom(string value)
    {
        return new TerminalOrderTrainingProvider(value);
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

    public static bool operator ==(TerminalOrderTrainingProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TerminalOrderTrainingProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TerminalOrderTrainingProvider value) => value.Value;

    public static explicit operator TerminalOrderTrainingProvider(string value) => new(value);

    internal class TerminalOrderTrainingProviderSerializer
        : JsonConverter<TerminalOrderTrainingProvider>
    {
        public override TerminalOrderTrainingProvider Read(
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
            return new TerminalOrderTrainingProvider(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TerminalOrderTrainingProvider value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TerminalOrderTrainingProvider ReadAsPropertyName(
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
            return new TerminalOrderTrainingProvider(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TerminalOrderTrainingProvider value,
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
        public const string Partner = "partner";

        public const string Payroc = "payroc";
    }
}
