using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StatusAdjustmentToStatus.StatusAdjustmentToStatusSerializer))]
[Serializable]
public readonly record struct StatusAdjustmentToStatus : IStringEnum
{
    public static readonly StatusAdjustmentToStatus Ready = new(Values.Ready);

    public static readonly StatusAdjustmentToStatus Pending = new(Values.Pending);

    public StatusAdjustmentToStatus(string value)
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
    public static StatusAdjustmentToStatus FromCustom(string value)
    {
        return new StatusAdjustmentToStatus(value);
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

    public static bool operator ==(StatusAdjustmentToStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StatusAdjustmentToStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(StatusAdjustmentToStatus value) => value.Value;

    public static explicit operator StatusAdjustmentToStatus(string value) => new(value);

    internal class StatusAdjustmentToStatusSerializer : JsonConverter<StatusAdjustmentToStatus>
    {
        public override StatusAdjustmentToStatus Read(
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
            return new StatusAdjustmentToStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            StatusAdjustmentToStatus value,
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
        public const string Ready = "ready";

        public const string Pending = "pending";
    }
}
