using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingRecipientRecipientType>))]
public readonly record struct FundingRecipientRecipientType : IStringEnum
{
    public static readonly FundingRecipientRecipientType PrivateCorporation = Custom(
        Values.PrivateCorporation
    );

    public static readonly FundingRecipientRecipientType PublicCorporation = Custom(
        Values.PublicCorporation
    );

    public static readonly FundingRecipientRecipientType NonProfit = Custom(Values.NonProfit);

    public static readonly FundingRecipientRecipientType Government = Custom(Values.Government);

    public static readonly FundingRecipientRecipientType PrivateLlc = Custom(Values.PrivateLlc);

    public static readonly FundingRecipientRecipientType PublicLlc = Custom(Values.PublicLlc);

    public static readonly FundingRecipientRecipientType PrivatePartnership = Custom(
        Values.PrivatePartnership
    );

    public static readonly FundingRecipientRecipientType PublicPartnership = Custom(
        Values.PublicPartnership
    );

    public static readonly FundingRecipientRecipientType SoleProprietor = Custom(
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
    public static FundingRecipientRecipientType Custom(string value)
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
