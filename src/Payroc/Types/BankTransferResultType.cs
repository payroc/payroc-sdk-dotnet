using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BankTransferResultType>))]
[Serializable]
public readonly record struct BankTransferResultType : IStringEnum
{
    public static readonly BankTransferResultType Payment = new(Values.Payment);

    public static readonly BankTransferResultType Refund = new(Values.Refund);

    public static readonly BankTransferResultType UnreferencedRefund = new(
        Values.UnreferencedRefund
    );

    public static readonly BankTransferResultType AccountVerification = new(
        Values.AccountVerification
    );

    public BankTransferResultType(string value)
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
    public static BankTransferResultType FromCustom(string value)
    {
        return new BankTransferResultType(value);
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

    public static bool operator ==(BankTransferResultType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BankTransferResultType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BankTransferResultType value) => value.Value;

    public static explicit operator BankTransferResultType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Payment = "payment";

        public const string Refund = "refund";

        public const string UnreferencedRefund = "unreferencedRefund";

        public const string AccountVerification = "accountVerification";
    }
}
