using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(PadSourceWithAccountTypeAccountType.PadSourceWithAccountTypeAccountTypeSerializer)
)]
[Serializable]
public readonly record struct PadSourceWithAccountTypeAccountType : IStringEnum
{
    public static readonly PadSourceWithAccountTypeAccountType Checking = new(Values.Checking);

    public static readonly PadSourceWithAccountTypeAccountType Savings = new(Values.Savings);

    public PadSourceWithAccountTypeAccountType(string value)
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
    public static PadSourceWithAccountTypeAccountType FromCustom(string value)
    {
        return new PadSourceWithAccountTypeAccountType(value);
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

    public static bool operator ==(PadSourceWithAccountTypeAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PadSourceWithAccountTypeAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PadSourceWithAccountTypeAccountType value) =>
        value.Value;

    public static explicit operator PadSourceWithAccountTypeAccountType(string value) => new(value);

    internal class PadSourceWithAccountTypeAccountTypeSerializer
        : JsonConverter<PadSourceWithAccountTypeAccountType>
    {
        public override PadSourceWithAccountTypeAccountType Read(
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
            return new PadSourceWithAccountTypeAccountType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PadSourceWithAccountTypeAccountType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PadSourceWithAccountTypeAccountType ReadAsPropertyName(
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
            return new PadSourceWithAccountTypeAccountType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PadSourceWithAccountTypeAccountType value,
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
        public const string Checking = "checking";

        public const string Savings = "savings";
    }
}
