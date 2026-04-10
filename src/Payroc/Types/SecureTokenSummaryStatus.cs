using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(SecureTokenSummaryStatus.SecureTokenSummaryStatusSerializer))]
[Serializable]
public readonly record struct SecureTokenSummaryStatus : IStringEnum
{
    public static readonly SecureTokenSummaryStatus NotValidated = new(Values.NotValidated);

    public static readonly SecureTokenSummaryStatus CvvValidated = new(Values.CvvValidated);

    public static readonly SecureTokenSummaryStatus ValidationFailed = new(Values.ValidationFailed);

    public static readonly SecureTokenSummaryStatus IssueNumberValidated = new(
        Values.IssueNumberValidated
    );

    public static readonly SecureTokenSummaryStatus CardNumberValidated = new(
        Values.CardNumberValidated
    );

    public static readonly SecureTokenSummaryStatus BankAccountValidated = new(
        Values.BankAccountValidated
    );

    public SecureTokenSummaryStatus(string value)
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
    public static SecureTokenSummaryStatus FromCustom(string value)
    {
        return new SecureTokenSummaryStatus(value);
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

    public static bool operator ==(SecureTokenSummaryStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecureTokenSummaryStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SecureTokenSummaryStatus value) => value.Value;

    public static explicit operator SecureTokenSummaryStatus(string value) => new(value);

    internal class SecureTokenSummaryStatusSerializer : JsonConverter<SecureTokenSummaryStatus>
    {
        public override SecureTokenSummaryStatus Read(
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
            return new SecureTokenSummaryStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            SecureTokenSummaryStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override SecureTokenSummaryStatus ReadAsPropertyName(
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
            return new SecureTokenSummaryStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            SecureTokenSummaryStatus value,
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
        public const string NotValidated = "notValidated";

        public const string CvvValidated = "cvvValidated";

        public const string ValidationFailed = "validationFailed";

        public const string IssueNumberValidated = "issueNumberValidated";

        public const string CardNumberValidated = "cardNumberValidated";

        public const string BankAccountValidated = "bankAccountValidated";
    }
}
