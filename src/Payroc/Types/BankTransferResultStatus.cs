using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BankTransferResultStatus>))]
[Serializable]
public readonly record struct BankTransferResultStatus : IStringEnum
{
    public static readonly BankTransferResultStatus Ready = new(Values.Ready);

    public static readonly BankTransferResultStatus Pending = new(Values.Pending);

    public static readonly BankTransferResultStatus Declined = new(Values.Declined);

    public static readonly BankTransferResultStatus Complete = new(Values.Complete);

    public static readonly BankTransferResultStatus Admin = new(Values.Admin);

    public static readonly BankTransferResultStatus Reversal = new(Values.Reversal);

    public static readonly BankTransferResultStatus Returned = new(Values.Returned);

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
    public static BankTransferResultStatus FromCustom(string value)
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

    public static explicit operator string(BankTransferResultStatus value) => value.Value;

    public static explicit operator BankTransferResultStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
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
