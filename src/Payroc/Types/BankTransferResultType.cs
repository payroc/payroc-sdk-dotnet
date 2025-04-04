using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BankTransferResultType>))]
public readonly record struct BankTransferResultType : IStringEnum
{
    public static readonly BankTransferResultType Payment = Custom(Values.Payment);

    public static readonly BankTransferResultType Refund = Custom(Values.Refund);

    public static readonly BankTransferResultType UnreferencedRefund = Custom(
        Values.UnreferencedRefund
    );

    public static readonly BankTransferResultType AccountVerification = Custom(
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
    public static BankTransferResultType Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Payment = "payment";

        public const string Refund = "refund";

        public const string UnreferencedRefund = "unreferencedRefund";

        public const string AccountVerification = "accountVerification";
    }
}
