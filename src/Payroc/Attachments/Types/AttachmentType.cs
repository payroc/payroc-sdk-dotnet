using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Attachments;

[JsonConverter(typeof(StringEnumSerializer<AttachmentType>))]
[Serializable]
public readonly record struct AttachmentType : IStringEnum
{
    public static readonly AttachmentType BankingEvidence = new(Values.BankingEvidence);

    public static readonly AttachmentType QuestionnairesAndLicenses = new(
        Values.QuestionnairesAndLicenses
    );

    public static readonly AttachmentType MerchantStatements = new(Values.MerchantStatements);

    public static readonly AttachmentType TaxDocuments = new(Values.TaxDocuments);

    public static readonly AttachmentType MpaOrAmendment = new(Values.MpaOrAmendment);

    public static readonly AttachmentType ProofOfBusiness = new(Values.ProofOfBusiness);

    public static readonly AttachmentType FinancialStatements = new(Values.FinancialStatements);

    public static readonly AttachmentType PersonalIdentification = new(
        Values.PersonalIdentification
    );

    public static readonly AttachmentType Other = new(Values.Other);

    public AttachmentType(string value)
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
    public static AttachmentType FromCustom(string value)
    {
        return new AttachmentType(value);
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

    public static bool operator ==(AttachmentType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AttachmentType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AttachmentType value) => value.Value;

    public static explicit operator AttachmentType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string BankingEvidence = "bankingEvidence";

        public const string QuestionnairesAndLicenses = "questionnairesAndLicenses";

        public const string MerchantStatements = "merchantStatements";

        public const string TaxDocuments = "taxDocuments";

        public const string MpaOrAmendment = "mpaOrAmendment";

        public const string ProofOfBusiness = "proofOfBusiness";

        public const string FinancialStatements = "financialStatements";

        public const string PersonalIdentification = "personalIdentification";

        public const string Other = "other";
    }
}
