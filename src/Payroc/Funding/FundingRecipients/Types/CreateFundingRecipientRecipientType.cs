using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Funding.FundingRecipients;

[JsonConverter(typeof(StringEnumSerializer<CreateFundingRecipientRecipientType>))]
public readonly record struct CreateFundingRecipientRecipientType : IStringEnum
{
    public static readonly CreateFundingRecipientRecipientType PrivateCorporation = Custom(
        Values.PrivateCorporation
    );

    public static readonly CreateFundingRecipientRecipientType PublicCorporation = Custom(
        Values.PublicCorporation
    );

    public static readonly CreateFundingRecipientRecipientType NonProfit = Custom(Values.NonProfit);

    public static readonly CreateFundingRecipientRecipientType Government = Custom(
        Values.Government
    );

    public static readonly CreateFundingRecipientRecipientType PrivateLlc = Custom(
        Values.PrivateLlc
    );

    public static readonly CreateFundingRecipientRecipientType PublicLlc = Custom(Values.PublicLlc);

    public static readonly CreateFundingRecipientRecipientType PrivatePartnership = Custom(
        Values.PrivatePartnership
    );

    public static readonly CreateFundingRecipientRecipientType PublicPartnership = Custom(
        Values.PublicPartnership
    );

    public static readonly CreateFundingRecipientRecipientType SoleProprietor = Custom(
        Values.SoleProprietor
    );

    public CreateFundingRecipientRecipientType(string value)
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
    public static CreateFundingRecipientRecipientType Custom(string value)
    {
        return new CreateFundingRecipientRecipientType(value);
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

    public static bool operator ==(CreateFundingRecipientRecipientType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateFundingRecipientRecipientType value1, string value2) =>
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
