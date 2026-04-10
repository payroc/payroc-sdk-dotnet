using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(TransactionResultStatus.TransactionResultStatusSerializer))]
[Serializable]
public readonly record struct TransactionResultStatus : IStringEnum
{
    public static readonly TransactionResultStatus Ready = new(Values.Ready);

    public static readonly TransactionResultStatus Pending = new(Values.Pending);

    public static readonly TransactionResultStatus Declined = new(Values.Declined);

    public static readonly TransactionResultStatus Complete = new(Values.Complete);

    public static readonly TransactionResultStatus Referral = new(Values.Referral);

    public static readonly TransactionResultStatus Pickup = new(Values.Pickup);

    public static readonly TransactionResultStatus Reversal = new(Values.Reversal);

    public static readonly TransactionResultStatus Admin = new(Values.Admin);

    public static readonly TransactionResultStatus Expired = new(Values.Expired);

    public static readonly TransactionResultStatus Accepted = new(Values.Accepted);

    public TransactionResultStatus(string value)
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
    public static TransactionResultStatus FromCustom(string value)
    {
        return new TransactionResultStatus(value);
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

    public static bool operator ==(TransactionResultStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionResultStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TransactionResultStatus value) => value.Value;

    public static explicit operator TransactionResultStatus(string value) => new(value);

    internal class TransactionResultStatusSerializer : JsonConverter<TransactionResultStatus>
    {
        public override TransactionResultStatus Read(
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
            return new TransactionResultStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TransactionResultStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TransactionResultStatus ReadAsPropertyName(
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
            return new TransactionResultStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TransactionResultStatus value,
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
        public const string Ready = "ready";

        public const string Pending = "pending";

        public const string Declined = "declined";

        public const string Complete = "complete";

        public const string Referral = "referral";

        public const string Pickup = "pickup";

        public const string Reversal = "reversal";

        public const string Admin = "admin";

        public const string Expired = "expired";

        public const string Accepted = "accepted";
    }
}
