using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionSummaryEntryMethod>))]
public readonly record struct TransactionSummaryEntryMethod : IStringEnum
{
    public static readonly TransactionSummaryEntryMethod BarcodeRead = Custom(Values.BarcodeRead);

    public static readonly TransactionSummaryEntryMethod SmartChipRead = Custom(
        Values.SmartChipRead
    );

    public static readonly TransactionSummaryEntryMethod SwipedOriginUnknown = Custom(
        Values.SwipedOriginUnknown
    );

    public static readonly TransactionSummaryEntryMethod ContactlessChip = Custom(
        Values.ContactlessChip
    );

    public static readonly TransactionSummaryEntryMethod Ecommerce = Custom(Values.Ecommerce);

    public static readonly TransactionSummaryEntryMethod ManuallyEntered = Custom(
        Values.ManuallyEntered
    );

    public static readonly TransactionSummaryEntryMethod ManuallyEnteredFallback = Custom(
        Values.ManuallyEnteredFallback
    );

    public static readonly TransactionSummaryEntryMethod Swiped = Custom(Values.Swiped);

    public static readonly TransactionSummaryEntryMethod SwipedFallback = Custom(
        Values.SwipedFallback
    );

    public static readonly TransactionSummaryEntryMethod SwipedError = Custom(Values.SwipedError);

    public static readonly TransactionSummaryEntryMethod ScannedCheckReader = Custom(
        Values.ScannedCheckReader
    );

    public static readonly TransactionSummaryEntryMethod CredentialOnFile = Custom(
        Values.CredentialOnFile
    );

    public static readonly TransactionSummaryEntryMethod Unknown = Custom(Values.Unknown);

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
    public static TransactionSummaryEntryMethod Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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
