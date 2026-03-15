using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ProcessingTerminalStatus.ProcessingTerminalStatusSerializer))]
[Serializable]
public readonly record struct ProcessingTerminalStatus : IStringEnum
{
    public static readonly ProcessingTerminalStatus Active = new(Values.Active);

    public static readonly ProcessingTerminalStatus Inactive = new(Values.Inactive);

    public ProcessingTerminalStatus(string value)
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
    public static ProcessingTerminalStatus FromCustom(string value)
    {
        return new ProcessingTerminalStatus(value);
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

    public static bool operator ==(ProcessingTerminalStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingTerminalStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingTerminalStatus value) => value.Value;

    public static explicit operator ProcessingTerminalStatus(string value) => new(value);

    internal class ProcessingTerminalStatusSerializer : JsonConverter<ProcessingTerminalStatus>
    {
        public override ProcessingTerminalStatus Read(
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
            return new ProcessingTerminalStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ProcessingTerminalStatus value,
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
        public const string Active = "active";

        public const string Inactive = "inactive";
    }
}
