using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(TransactionSummaryEntryMethod.TransactionSummaryEntryMethodSerializer))]
[Serializable]
public readonly record struct TransactionSummaryEntryMethod : IStringEnum
{
    public static readonly TransactionSummaryEntryMethod BarcodeRead = new(Values.BarcodeRead);

    public static readonly TransactionSummaryEntryMethod SmartChipRead = new(Values.SmartChipRead);

    public static readonly TransactionSummaryEntryMethod SwipedOriginUnknown = new(
        Values.SwipedOriginUnknown
    );

    public static readonly TransactionSummaryEntryMethod ContactlessChip = new(
        Values.ContactlessChip
    );

    public static readonly TransactionSummaryEntryMethod Ecommerce = new(Values.Ecommerce);

    public static readonly TransactionSummaryEntryMethod ManuallyEntered = new(
        Values.ManuallyEntered
    );

    public static readonly TransactionSummaryEntryMethod ManuallyEnteredFallback = new(
        Values.ManuallyEnteredFallback
    );

    public static readonly TransactionSummaryEntryMethod Swiped = new(Values.Swiped);

    public static readonly TransactionSummaryEntryMethod SwipedFallback = new(
        Values.SwipedFallback
    );

    public static readonly TransactionSummaryEntryMethod SwipedError = new(Values.SwipedError);

    public static readonly TransactionSummaryEntryMethod ScannedCheckReader = new(
        Values.ScannedCheckReader
    );

    public static readonly TransactionSummaryEntryMethod CredentialOnFile = new(
        Values.CredentialOnFile
    );

    public static readonly TransactionSummaryEntryMethod Unknown = new(Values.Unknown);

    public TransactionSummaryEntryMethod(string value)
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
    public static TransactionSummaryEntryMethod FromCustom(string value)
    {
        return new TransactionSummaryEntryMethod(value);
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

    public static bool operator ==(TransactionSummaryEntryMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionSummaryEntryMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TransactionSummaryEntryMethod value) => value.Value;

    public static explicit operator TransactionSummaryEntryMethod(string value) => new(value);

    internal class TransactionSummaryEntryMethodSerializer
        : JsonConverter<TransactionSummaryEntryMethod>
    {
        public override TransactionSummaryEntryMethod Read(
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
            return new TransactionSummaryEntryMethod(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TransactionSummaryEntryMethod value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TransactionSummaryEntryMethod ReadAsPropertyName(
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
            return new TransactionSummaryEntryMethod(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TransactionSummaryEntryMethod value,
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
        public const string BarcodeRead = "barcodeRead";

        public const string SmartChipRead = "smartChipRead";

        public const string SwipedOriginUnknown = "swipedOriginUnknown";

        public const string ContactlessChip = "contactlessChip";

        public const string Ecommerce = "ecommerce";

        public const string ManuallyEntered = "manuallyEntered";

        public const string ManuallyEnteredFallback = "manuallyEnteredFallback";

        public const string Swiped = "swiped";

        public const string SwipedFallback = "swipedFallback";

        public const string SwipedError = "swipedError";

        public const string ScannedCheckReader = "scannedCheckReader";

        public const string CredentialOnFile = "credentialOnFile";

        public const string Unknown = "unknown";
    }
}
