using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionEntryMethod>))]
public readonly record struct TransactionEntryMethod : IStringEnum
{
    public static readonly TransactionEntryMethod BarcodeRead = Custom(Values.BarcodeRead);

    public static readonly TransactionEntryMethod SmartChipRead = Custom(Values.SmartChipRead);

    public static readonly TransactionEntryMethod SwipedOriginUnknown = Custom(
        Values.SwipedOriginUnknown
    );

    public static readonly TransactionEntryMethod ContactlessChip = Custom(Values.ContactlessChip);

    public static readonly TransactionEntryMethod Ecommerce = Custom(Values.Ecommerce);

    public static readonly TransactionEntryMethod ManuallyEntered = Custom(Values.ManuallyEntered);

    public static readonly TransactionEntryMethod ManuallyEnteredFallback = Custom(
        Values.ManuallyEnteredFallback
    );

    public static readonly TransactionEntryMethod Swiped = Custom(Values.Swiped);

    public static readonly TransactionEntryMethod SwipedFallback = Custom(Values.SwipedFallback);

    public static readonly TransactionEntryMethod SwipedError = Custom(Values.SwipedError);

    public static readonly TransactionEntryMethod ScannedCheckReader = Custom(
        Values.ScannedCheckReader
    );

    public static readonly TransactionEntryMethod CredentialOnFile = Custom(
        Values.CredentialOnFile
    );

    public static readonly TransactionEntryMethod Unknown = Custom(Values.Unknown);

    public TransactionEntryMethod(string value)
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
    public static TransactionEntryMethod Custom(string value)
    {
        return new TransactionEntryMethod(value);
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

    public static bool operator ==(TransactionEntryMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionEntryMethod value1, string value2) =>
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
