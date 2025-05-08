using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardSummaryType>))]
public readonly record struct CardSummaryType : IStringEnum
{
    public static readonly CardSummaryType Visa = new(Values.Visa);

    public static readonly CardSummaryType MasterCard = new(Values.MasterCard);

    public static readonly CardSummaryType Discover = new(Values.Discover);

    public static readonly CardSummaryType Debit = new(Values.Debit);

    public static readonly CardSummaryType Ebt = new(Values.Ebt);

    public static readonly CardSummaryType WrightExpress = new(Values.WrightExpress);

    public static readonly CardSummaryType Voyager = new(Values.Voyager);

    public static readonly CardSummaryType Amex = new(Values.Amex);

    public static readonly CardSummaryType PrivateLabel = new(Values.PrivateLabel);

    public static readonly CardSummaryType StoredValue = new(Values.StoredValue);

    public static readonly CardSummaryType DiscoverRetained = new(Values.DiscoverRetained);

    public static readonly CardSummaryType JcbNonSettled = new(Values.JcbNonSettled);

    public static readonly CardSummaryType DinersClub = new(Values.DinersClub);

    public static readonly CardSummaryType AmexOptBlue = new(Values.AmexOptBlue);

    public static readonly CardSummaryType Fuelman = new(Values.Fuelman);

    public static readonly CardSummaryType Unknown = new(Values.Unknown);

    public CardSummaryType(string value)
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
    public static CardSummaryType FromCustom(string value)
    {
        return new CardSummaryType(value);
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

    public static bool operator ==(CardSummaryType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CardSummaryType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CardSummaryType value) => value.Value;

    public static explicit operator CardSummaryType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Visa = "visa";

        public const string MasterCard = "masterCard";

        public const string Discover = "discover";

        public const string Debit = "debit";

        public const string Ebt = "ebt";

        public const string WrightExpress = "wrightExpress";

        public const string Voyager = "voyager";

        public const string Amex = "amex";

        public const string PrivateLabel = "privateLabel";

        public const string StoredValue = "storedValue";

        public const string DiscoverRetained = "discoverRetained";

        public const string JcbNonSettled = "jcbNonSettled";

        public const string DinersClub = "dinersClub";

        public const string AmexOptBlue = "amexOptBlue";

        public const string Fuelman = "fuelman";

        public const string Unknown = "unknown";
    }
}
