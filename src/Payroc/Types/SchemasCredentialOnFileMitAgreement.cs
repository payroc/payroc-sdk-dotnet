using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(SchemasCredentialOnFileMitAgreement.SchemasCredentialOnFileMitAgreementSerializer)
)]
[Serializable]
public readonly record struct SchemasCredentialOnFileMitAgreement : IStringEnum
{
    public static readonly SchemasCredentialOnFileMitAgreement Unscheduled = new(
        Values.Unscheduled
    );

    public static readonly SchemasCredentialOnFileMitAgreement Recurring = new(Values.Recurring);

    public static readonly SchemasCredentialOnFileMitAgreement Installment = new(
        Values.Installment
    );

    public SchemasCredentialOnFileMitAgreement(string value)
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
    public static SchemasCredentialOnFileMitAgreement FromCustom(string value)
    {
        return new SchemasCredentialOnFileMitAgreement(value);
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

    public static bool operator ==(SchemasCredentialOnFileMitAgreement value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SchemasCredentialOnFileMitAgreement value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SchemasCredentialOnFileMitAgreement value) =>
        value.Value;

    public static explicit operator SchemasCredentialOnFileMitAgreement(string value) => new(value);

    internal class SchemasCredentialOnFileMitAgreementSerializer
        : JsonConverter<SchemasCredentialOnFileMitAgreement>
    {
        public override SchemasCredentialOnFileMitAgreement Read(
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
            return new SchemasCredentialOnFileMitAgreement(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SchemasCredentialOnFileMitAgreement value,
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
        public const string Unscheduled = "unscheduled";

        public const string Recurring = "recurring";

        public const string Installment = "installment";
    }
}
