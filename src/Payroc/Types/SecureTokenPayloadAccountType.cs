using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SecureTokenPayloadAccountType.SecureTokenPayloadAccountTypeSerializer))]
[Serializable]
public readonly record struct SecureTokenPayloadAccountType : IStringEnum
{
    public static readonly SecureTokenPayloadAccountType Checking = new(Values.Checking);

    public static readonly SecureTokenPayloadAccountType Savings = new(Values.Savings);

    public SecureTokenPayloadAccountType(string value)
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
    public static SecureTokenPayloadAccountType FromCustom(string value)
    {
        return new SecureTokenPayloadAccountType(value);
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

    public static bool operator ==(SecureTokenPayloadAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecureTokenPayloadAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SecureTokenPayloadAccountType value) => value.Value;

    public static explicit operator SecureTokenPayloadAccountType(string value) => new(value);

    internal class SecureTokenPayloadAccountTypeSerializer
        : JsonConverter<SecureTokenPayloadAccountType>
    {
        public override SecureTokenPayloadAccountType Read(
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
            return new SecureTokenPayloadAccountType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SecureTokenPayloadAccountType value,
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
