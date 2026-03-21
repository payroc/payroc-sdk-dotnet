using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Tokenization.SecureTokens;

[JsonConverter(typeof(TokenizationRequestMitAgreement.TokenizationRequestMitAgreementSerializer))]
[Serializable]
public readonly record struct TokenizationRequestMitAgreement : IStringEnum
{
    public static readonly TokenizationRequestMitAgreement Unscheduled = new(Values.Unscheduled);

    public static readonly TokenizationRequestMitAgreement Recurring = new(Values.Recurring);

    public static readonly TokenizationRequestMitAgreement Installment = new(Values.Installment);

    public TokenizationRequestMitAgreement(string value)
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
    public static TokenizationRequestMitAgreement FromCustom(string value)
    {
        return new TokenizationRequestMitAgreement(value);
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

    public static bool operator ==(TokenizationRequestMitAgreement value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TokenizationRequestMitAgreement value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TokenizationRequestMitAgreement value) => value.Value;

    public static explicit operator TokenizationRequestMitAgreement(string value) => new(value);

    internal class TokenizationRequestMitAgreementSerializer
        : JsonConverter<TokenizationRequestMitAgreement>
    {
        public override TokenizationRequestMitAgreement Read(
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
            return new TokenizationRequestMitAgreement(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TokenizationRequestMitAgreement value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TokenizationRequestMitAgreement ReadAsPropertyName(
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
            return new TokenizationRequestMitAgreement(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TokenizationRequestMitAgreement value,
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
        public const string Unscheduled = "unscheduled";

        public const string Recurring = "recurring";

        public const string Installment = "installment";
    }
}
