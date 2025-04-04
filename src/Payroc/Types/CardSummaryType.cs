using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CardSummaryType>))]
public readonly record struct CardSummaryType : IStringEnum
{
    public static readonly CardSummaryType Visa = Custom(Values.Visa);

    public static readonly CardSummaryType MasterCard = Custom(Values.MasterCard);

    public static readonly CardSummaryType Discover = Custom(Values.Discover);

    public static readonly CardSummaryType Debit = Custom(Values.Debit);

    public static readonly CardSummaryType Ebt = Custom(Values.Ebt);

    public static readonly CardSummaryType WrightExpress = Custom(Values.WrightExpress);

    public static readonly CardSummaryType Voyager = Custom(Values.Voyager);

    public static readonly CardSummaryType Amex = Custom(Values.Amex);

    public static readonly CardSummaryType PrivateLabel = Custom(Values.PrivateLabel);

    public static readonly CardSummaryType StoredValue = Custom(Values.StoredValue);

    public static readonly CardSummaryType DiscoverRetained = Custom(Values.DiscoverRetained);

    public static readonly CardSummaryType JcbNonSettled = Custom(Values.JcbNonSettled);

    public static readonly CardSummaryType DinersClub = Custom(Values.DinersClub);

    public static readonly CardSummaryType AmexOptBlue = Custom(Values.AmexOptBlue);

    public static readonly CardSummaryType Fuelman = Custom(Values.Fuelman);

    public static readonly CardSummaryType Unknown = Custom(Values.Unknown);

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
    public static CardSummaryType Custom(string value)
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
