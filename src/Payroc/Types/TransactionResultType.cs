using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionResultType>))]
public readonly record struct TransactionResultType : IStringEnum
{
    public static readonly TransactionResultType Sale = Custom(Values.Sale);

    public static readonly TransactionResultType Refund = Custom(Values.Refund);

    public static readonly TransactionResultType PreAuthorization = Custom(Values.PreAuthorization);

    public static readonly TransactionResultType PreAuthorizationCompletion = Custom(
        Values.PreAuthorizationCompletion
    );

    public TransactionResultType(string value)
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
    public static TransactionResultType Custom(string value)
    {
        return new TransactionResultType(value);
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

    public static bool operator ==(TransactionResultType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionResultType value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Sale = "sale";

        public const string Refund = "refund";

        public const string PreAuthorization = "preAuthorization";

        public const string PreAuthorizationCompletion = "preAuthorizationCompletion";
    }
}
