using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionResultEbtType>))]
[Serializable]
public readonly record struct TransactionResultEbtType : IStringEnum
{
    public static readonly TransactionResultEbtType CashPurchase = new(Values.CashPurchase);

    public static readonly TransactionResultEbtType CashPurchaseWithCashback = new(
        Values.CashPurchaseWithCashback
    );

    public static readonly TransactionResultEbtType FoodStampPurchase = new(
        Values.FoodStampPurchase
    );

    public static readonly TransactionResultEbtType FoodStampVoucherPurchase = new(
        Values.FoodStampVoucherPurchase
    );

    public static readonly TransactionResultEbtType FoodStampReturn = new(Values.FoodStampReturn);

    public static readonly TransactionResultEbtType FoodStampVoucherReturn = new(
        Values.FoodStampVoucherReturn
    );

    public static readonly TransactionResultEbtType CashBalanceInquiry = new(
        Values.CashBalanceInquiry
    );

    public static readonly TransactionResultEbtType FoodStampBalanceInquiry = new(
        Values.FoodStampBalanceInquiry
    );

    public static readonly TransactionResultEbtType CashWithdrawal = new(Values.CashWithdrawal);

    public TransactionResultEbtType(string value)
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
    public static TransactionResultEbtType FromCustom(string value)
    {
        return new TransactionResultEbtType(value);
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

    public static bool operator ==(TransactionResultEbtType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionResultEbtType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TransactionResultEbtType value) => value.Value;

    public static explicit operator TransactionResultEbtType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CashPurchase = "cashPurchase";

        public const string CashPurchaseWithCashback = "cashPurchaseWithCashback";

        public const string FoodStampPurchase = "foodStampPurchase";

        public const string FoodStampVoucherPurchase = "foodStampVoucherPurchase";

        public const string FoodStampReturn = "foodStampReturn";

        public const string FoodStampVoucherReturn = "foodStampVoucherReturn";

        public const string CashBalanceInquiry = "cashBalanceInquiry";

        public const string FoodStampBalanceInquiry = "foodStampBalanceInquiry";

        public const string CashWithdrawal = "cashWithdrawal";
    }
}
