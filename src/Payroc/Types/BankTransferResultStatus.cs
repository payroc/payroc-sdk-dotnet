using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BankTransferResultStatus>))]
public readonly record struct BankTransferResultStatus : IStringEnum
{
    public static readonly BankTransferResultStatus Ready = Custom(Values.Ready);

    public static readonly BankTransferResultStatus Pending = Custom(Values.Pending);

    public static readonly BankTransferResultStatus Declined = Custom(Values.Declined);

    public static readonly BankTransferResultStatus Complete = Custom(Values.Complete);

    public static readonly BankTransferResultStatus Admin = Custom(Values.Admin);

    public static readonly BankTransferResultStatus Reversal = Custom(Values.Reversal);

    public static readonly BankTransferResultStatus Returned = Custom(Values.Returned);

    public BankTransferResultStatus(string value)
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
    public static BankTransferResultStatus Custom(string value)
    {
        return new BankTransferResultStatus(value);
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

    public static bool operator ==(BankTransferResultStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BankTransferResultStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Ready = "ready";

        public const string Pending = "pending";

        public const string Declined = "declined";

        public const string Complete = "complete";

        public const string Admin = "admin";

        public const string Reversal = "reversal";

        public const string Returned = "returned";
    }
}
