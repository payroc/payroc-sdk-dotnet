using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Attachments;

[JsonConverter(
    typeof(StringEnumSerializer<UploadToProcessingAccountAttachmentsRequestAttachmentType>)
)]
[Serializable]
public readonly record struct UploadToProcessingAccountAttachmentsRequestAttachmentType
    : IStringEnum
{
    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType BankingEvidence =
        new(Values.BankingEvidence);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType QuestionnairesAndLicenses =
        new(Values.QuestionnairesAndLicenses);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType MerchantStatements =
        new(Values.MerchantStatements);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType TaxDocuments =
        new(Values.TaxDocuments);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType MpaOrAmendment =
        new(Values.MpaOrAmendment);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType ProofOfBusiness =
        new(Values.ProofOfBusiness);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType FinancialStatements =
        new(Values.FinancialStatements);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType PersonalIdentification =
        new(Values.PersonalIdentification);

    public static readonly UploadToProcessingAccountAttachmentsRequestAttachmentType Other = new(
        Values.Other
    );

    public UploadToProcessingAccountAttachmentsRequestAttachmentType(string value)
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
    public static UploadToProcessingAccountAttachmentsRequestAttachmentType FromCustom(string value)
    {
        return new UploadToProcessingAccountAttachmentsRequestAttachmentType(value);
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
        UploadToProcessingAccountAttachmentsRequestAttachmentType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UploadToProcessingAccountAttachmentsRequestAttachmentType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        UploadToProcessingAccountAttachmentsRequestAttachmentType value
    ) => value.Value;

    public static explicit operator UploadToProcessingAccountAttachmentsRequestAttachmentType(
        string value
    ) => new(value);

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
