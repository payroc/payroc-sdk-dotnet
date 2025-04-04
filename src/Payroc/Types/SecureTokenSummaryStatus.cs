using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SecureTokenSummaryStatus>))]
public readonly record struct SecureTokenSummaryStatus : IStringEnum
{
    public static readonly SecureTokenSummaryStatus NotValidated = Custom(Values.NotValidated);

    public static readonly SecureTokenSummaryStatus CvvValidated = Custom(Values.CvvValidated);

    public static readonly SecureTokenSummaryStatus ValidationFailed = Custom(
        Values.ValidationFailed
    );

    public static readonly SecureTokenSummaryStatus IssueNumberValidated = Custom(
        Values.IssueNumberValidated
    );

    public static readonly SecureTokenSummaryStatus CardNumberValidated = Custom(
        Values.CardNumberValidated
    );

    public static readonly SecureTokenSummaryStatus BankAccountValidated = Custom(
        Values.BankAccountValidated
    );

    public SecureTokenSummaryStatus(string value)
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
    public static SecureTokenSummaryStatus Custom(string value)
    {
        return new SecureTokenSummaryStatus(value);
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

    public static bool operator ==(SecureTokenSummaryStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SecureTokenSummaryStatus value1, string value2) =>
        !value1.Value.Equals(value2);

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
