using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(AchPayloadAccountType.AchPayloadAccountTypeSerializer))]
[Serializable]
public readonly record struct AchPayloadAccountType : IStringEnum
{
    public static readonly AchPayloadAccountType Checking = new(Values.Checking);

    public static readonly AchPayloadAccountType Savings = new(Values.Savings);

    public AchPayloadAccountType(string value)
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
    public static AchPayloadAccountType FromCustom(string value)
    {
        return new AchPayloadAccountType(value);
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

    public static bool operator ==(AchPayloadAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AchPayloadAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AchPayloadAccountType value) => value.Value;

    public static explicit operator AchPayloadAccountType(string value) => new(value);

    internal class AchPayloadAccountTypeSerializer : JsonConverter<AchPayloadAccountType>
    {
        public override AchPayloadAccountType Read(
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
            return new AchPayloadAccountType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AchPayloadAccountType value,
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
        public const string Checking = "checking";

        public const string Savings = "savings";
    }
}
