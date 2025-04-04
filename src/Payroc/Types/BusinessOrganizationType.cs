using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BusinessOrganizationType>))]
public readonly record struct BusinessOrganizationType : IStringEnum
{
    public static readonly BusinessOrganizationType PrivateCorporation = Custom(
        Values.PrivateCorporation
    );

    public static readonly BusinessOrganizationType PublicCorporation = Custom(
        Values.PublicCorporation
    );

    public static readonly BusinessOrganizationType NonProfit = Custom(Values.NonProfit);

    public static readonly BusinessOrganizationType PrivateLlc = Custom(Values.PrivateLlc);

    public static readonly BusinessOrganizationType PublicLlc = Custom(Values.PublicLlc);

    public static readonly BusinessOrganizationType PrivatePartnership = Custom(
        Values.PrivatePartnership
    );

    public static readonly BusinessOrganizationType PublicPartnership = Custom(
        Values.PublicPartnership
    );

    public static readonly BusinessOrganizationType SoleProprietor = Custom(Values.SoleProprietor);

    public BusinessOrganizationType(string value)
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
    public static BusinessOrganizationType Custom(string value)
    {
        return new BusinessOrganizationType(value);
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

    public static bool operator ==(BusinessOrganizationType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BusinessOrganizationType value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string PrivateCorporation = "privateCorporation";

        public const string PublicCorporation = "publicCorporation";

        public const string NonProfit = "nonProfit";

        public const string PrivateLlc = "privateLlc";

        public const string PublicLlc = "publicLlc";

        public const string PrivatePartnership = "privatePartnership";

        public const string PublicPartnership = "publicPartnership";

        public const string SoleProprietor = "soleProprietor";
    }
}
