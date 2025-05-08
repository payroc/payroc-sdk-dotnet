using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingRecipientRecipientType>))]
public readonly record struct FundingRecipientRecipientType : IStringEnum
{
    public static readonly FundingRecipientRecipientType PrivateCorporation = new(
        Values.PrivateCorporation
    );

    public static readonly FundingRecipientRecipientType PublicCorporation = new(
        Values.PublicCorporation
    );

    public static readonly FundingRecipientRecipientType NonProfit = new(Values.NonProfit);

    public static readonly FundingRecipientRecipientType Government = new(Values.Government);

    public static readonly FundingRecipientRecipientType PrivateLlc = new(Values.PrivateLlc);

    public static readonly FundingRecipientRecipientType PublicLlc = new(Values.PublicLlc);

    public static readonly FundingRecipientRecipientType PrivatePartnership = new(
        Values.PrivatePartnership
    );

    public static readonly FundingRecipientRecipientType PublicPartnership = new(
        Values.PublicPartnership
    );

    public static readonly FundingRecipientRecipientType SoleProprietor = new(
        Values.SoleProprietor
    );

    public FundingRecipientRecipientType(string value)
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
    public static FundingRecipientRecipientType FromCustom(string value)
    {
        return new FundingRecipientRecipientType(value);
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

    public static bool operator ==(FundingRecipientRecipientType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingRecipientRecipientType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FundingRecipientRecipientType value) => value.Value;

    public static explicit operator FundingRecipientRecipientType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string PrivateCorporation = "privateCorporation";

        public const string PublicCorporation = "publicCorporation";

        public const string NonProfit = "nonProfit";

        public const string Government = "government";

        public const string PrivateLlc = "privateLlc";

        public const string PublicLlc = "publicLlc";

        public const string PrivatePartnership = "privatePartnership";

        public const string PublicPartnership = "publicPartnership";

        public const string SoleProprietor = "soleProprietor";
    }
}
