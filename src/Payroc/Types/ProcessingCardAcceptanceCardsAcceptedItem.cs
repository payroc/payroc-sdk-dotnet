using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingCardAcceptanceCardsAcceptedItem>))]
public readonly record struct ProcessingCardAcceptanceCardsAcceptedItem : IStringEnum
{
    public static readonly ProcessingCardAcceptanceCardsAcceptedItem Visa = Custom(Values.Visa);

    public static readonly ProcessingCardAcceptanceCardsAcceptedItem Mastercard = Custom(
        Values.Mastercard
    );

    public static readonly ProcessingCardAcceptanceCardsAcceptedItem Discover = Custom(
        Values.Discover
    );

    public static readonly ProcessingCardAcceptanceCardsAcceptedItem AmexOptBlue = Custom(
        Values.AmexOptBlue
    );

    public ProcessingCardAcceptanceCardsAcceptedItem(string value)
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
    public static ProcessingCardAcceptanceCardsAcceptedItem Custom(string value)
    {
        return new ProcessingCardAcceptanceCardsAcceptedItem(value);
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

    public static bool operator ==(
        ProcessingCardAcceptanceCardsAcceptedItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ProcessingCardAcceptanceCardsAcceptedItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Visa = "visa";

        public const string Mastercard = "mastercard";

        public const string Discover = "discover";

        public const string AmexOptBlue = "amexOptBlue";
    }
}
