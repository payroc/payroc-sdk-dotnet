using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SecureTokenStatus>))]
public readonly record struct SecureTokenStatus : IStringEnum
{
    public static readonly SecureTokenStatus NotValidated = new(Values.NotValidated);

    public static readonly SecureTokenStatus CvvValidated = new(Values.CvvValidated);

    public static readonly SecureTokenStatus ValidationFailed = new(Values.ValidationFailed);

    public static readonly SecureTokenStatus IssueNumberValidated = new(
        Values.IssueNumberValidated
    );

    public static readonly SecureTokenStatus CardNumberValidated = new(Values.CardNumberValidated);

    public static readonly SecureTokenStatus BankAccountValidated = new(
        Values.BankAccountValidated
    );

    public SecureTokenStatus(string value)
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
    public static SecureTokenStatus FromCustom(string value)
    {
        return new SecureTokenStatus(value);
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

    public static bool operator ==(SecureTokenStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecureTokenStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SecureTokenStatus value) => value.Value;

    public static explicit operator SecureTokenStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string NotValidated = "notValidated";

        public const string CvvValidated = "cvvValidated";

        public const string ValidationFailed = "validationFailed";

        public const string IssueNumberValidated = "issueNumberValidated";

        public const string CardNumberValidated = "cardNumberValidated";

        public const string BankAccountValidated = "bankAccountValidated";
    }
}
