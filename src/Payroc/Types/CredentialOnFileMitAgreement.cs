using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CredentialOnFileMitAgreement>))]
public readonly record struct CredentialOnFileMitAgreement : IStringEnum
{
    public static readonly CredentialOnFileMitAgreement Unscheduled = Custom(Values.Unscheduled);

    public static readonly CredentialOnFileMitAgreement Recurring = Custom(Values.Recurring);

    public static readonly CredentialOnFileMitAgreement Installment = Custom(Values.Installment);

    public CredentialOnFileMitAgreement(string value)
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
    public static CredentialOnFileMitAgreement Custom(string value)
    {
        return new CredentialOnFileMitAgreement(value);
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

    public static bool operator ==(CredentialOnFileMitAgreement value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CredentialOnFileMitAgreement value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Unscheduled = "unscheduled";

        public const string Recurring = "recurring";

        public const string Installment = "installment";
    }
}
