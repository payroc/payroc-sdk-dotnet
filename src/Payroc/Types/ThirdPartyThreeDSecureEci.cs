using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(ThirdPartyThreeDSecureEci.ThirdPartyThreeDSecureEciSerializer))]
[Serializable]
public readonly record struct ThirdPartyThreeDSecureEci : IStringEnum
{
    public static readonly ThirdPartyThreeDSecureEci FullyAuthenticated = new(
        Values.FullyAuthenticated
    );

    public static readonly ThirdPartyThreeDSecureEci AttemptedAuthentication = new(
        Values.AttemptedAuthentication
    );

    public ThirdPartyThreeDSecureEci(string value)
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
    public static ThirdPartyThreeDSecureEci FromCustom(string value)
    {
        return new ThirdPartyThreeDSecureEci(value);
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

    public static bool operator ==(ThirdPartyThreeDSecureEci value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ThirdPartyThreeDSecureEci value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ThirdPartyThreeDSecureEci value) => value.Value;

    public static explicit operator ThirdPartyThreeDSecureEci(string value) => new(value);

    internal class ThirdPartyThreeDSecureEciSerializer : JsonConverter<ThirdPartyThreeDSecureEci>
    {
        public override ThirdPartyThreeDSecureEci Read(
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
            return new ThirdPartyThreeDSecureEci(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ThirdPartyThreeDSecureEci value,
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
        public const string FullyAuthenticated = "fullyAuthenticated";

        public const string AttemptedAuthentication = "attemptedAuthentication";
    }
}
