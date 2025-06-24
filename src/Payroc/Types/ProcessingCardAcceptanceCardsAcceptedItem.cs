using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingCardAcceptanceCardsAcceptedItem>))]
[Serializable]
public readonly record struct ProcessingCardAcceptanceCardsAcceptedItem : IStringEnum
{
    public static readonly ProcessingCardAcceptanceCardsAcceptedItem Visa = new(Values.Visa);

    public static readonly ProcessingCardAcceptanceCardsAcceptedItem Mastercard = new(
        Values.Mastercard
    );

    public static readonly ProcessingCardAcceptanceCardsAcceptedItem Discover = new(
        Values.Discover
    );

    public static readonly ProcessingCardAcceptanceCardsAcceptedItem AmexOptBlue = new(
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
    public static ProcessingCardAcceptanceCardsAcceptedItem FromCustom(string value)
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

    public static explicit operator string(ProcessingCardAcceptanceCardsAcceptedItem value) =>
        value.Value;

    public static explicit operator ProcessingCardAcceptanceCardsAcceptedItem(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Visa = "visa";

        public const string Mastercard = "mastercard";

        public const string Discover = "discover";

        public const string AmexOptBlue = "amexOptBlue";
    }
}
