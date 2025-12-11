using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CredentialOnFileMitAgreement>))]
[Serializable]
public readonly record struct CredentialOnFileMitAgreement : IStringEnum
{
    public static readonly CredentialOnFileMitAgreement Unscheduled = new(Values.Unscheduled);

    public static readonly CredentialOnFileMitAgreement Recurring = new(Values.Recurring);

    public static readonly CredentialOnFileMitAgreement Installment = new(Values.Installment);

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
    public static CredentialOnFileMitAgreement FromCustom(string value)
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

    public static explicit operator string(CredentialOnFileMitAgreement value) => value.Value;

    public static explicit operator CredentialOnFileMitAgreement(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Unscheduled = "unscheduled";

        public const string Recurring = "recurring";

        public const string Installment = "installment";
    }
}
