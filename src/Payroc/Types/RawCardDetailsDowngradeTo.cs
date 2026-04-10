using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(RawCardDetailsDowngradeTo.RawCardDetailsDowngradeToSerializer))]
[Serializable]
public readonly record struct RawCardDetailsDowngradeTo : IStringEnum
{
    public static readonly RawCardDetailsDowngradeTo Keyed = new(Values.Keyed);

    public static readonly RawCardDetailsDowngradeTo Swiped = new(Values.Swiped);

    public RawCardDetailsDowngradeTo(string value)
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
    public static RawCardDetailsDowngradeTo FromCustom(string value)
    {
        return new RawCardDetailsDowngradeTo(value);
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

    public static bool operator ==(RawCardDetailsDowngradeTo value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RawCardDetailsDowngradeTo value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RawCardDetailsDowngradeTo value) => value.Value;

    public static explicit operator RawCardDetailsDowngradeTo(string value) => new(value);

    internal class RawCardDetailsDowngradeToSerializer : JsonConverter<RawCardDetailsDowngradeTo>
    {
        public override RawCardDetailsDowngradeTo Read(
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
            return new RawCardDetailsDowngradeTo(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RawCardDetailsDowngradeTo value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RawCardDetailsDowngradeTo ReadAsPropertyName(
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
            return new RawCardDetailsDowngradeTo(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RawCardDetailsDowngradeTo value,
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
        public const string Keyed = "keyed";

        public const string Swiped = "swiped";
    }
}
