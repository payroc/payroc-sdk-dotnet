using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(ProcessingTerminalSecurityAvsLevel.ProcessingTerminalSecurityAvsLevelSerializer)
)]
[Serializable]
public readonly record struct ProcessingTerminalSecurityAvsLevel : IStringEnum
{
    public static readonly ProcessingTerminalSecurityAvsLevel FullAddress = new(Values.FullAddress);

    public static readonly ProcessingTerminalSecurityAvsLevel PostalCode = new(Values.PostalCode);

    public ProcessingTerminalSecurityAvsLevel(string value)
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
    public static ProcessingTerminalSecurityAvsLevel FromCustom(string value)
    {
        return new ProcessingTerminalSecurityAvsLevel(value);
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

    public static bool operator ==(ProcessingTerminalSecurityAvsLevel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingTerminalSecurityAvsLevel value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingTerminalSecurityAvsLevel value) => value.Value;

    public static explicit operator ProcessingTerminalSecurityAvsLevel(string value) => new(value);

    internal class ProcessingTerminalSecurityAvsLevelSerializer
        : JsonConverter<ProcessingTerminalSecurityAvsLevel>
    {
        public override ProcessingTerminalSecurityAvsLevel Read(
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
            return new ProcessingTerminalSecurityAvsLevel(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProcessingTerminalSecurityAvsLevel value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override ProcessingTerminalSecurityAvsLevel ReadAsPropertyName(
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
            return new ProcessingTerminalSecurityAvsLevel(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            ProcessingTerminalSecurityAvsLevel value,
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
        public const string FullAddress = "fullAddress";

        public const string PostalCode = "postalCode";
    }
}
