using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionResultEbtType>))]
public readonly record struct TransactionResultEbtType : IStringEnum
{
    public static readonly TransactionResultEbtType CashPurchase = Custom(Values.CashPurchase);

    public static readonly TransactionResultEbtType CashPurchaseWithCashback = Custom(
        Values.CashPurchaseWithCashback
    );

    public static readonly TransactionResultEbtType FoodStampPurchase = Custom(
        Values.FoodStampPurchase
    );

    public static readonly TransactionResultEbtType FoodStampVoucherPurchase = Custom(
        Values.FoodStampVoucherPurchase
    );

    public static readonly TransactionResultEbtType FoodStampReturn = Custom(
        Values.FoodStampReturn
    );

    public static readonly TransactionResultEbtType FoodStampVoucherReturn = Custom(
        Values.FoodStampVoucherReturn
    );

    public static readonly TransactionResultEbtType CashBalanceInquiry = Custom(
        Values.CashBalanceInquiry
    );

    public static readonly TransactionResultEbtType FoodStampBalanceInquiry = Custom(
        Values.FoodStampBalanceInquiry
    );

    public static readonly TransactionResultEbtType CashWithdrawal = Custom(Values.CashWithdrawal);

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
    public static TransactionResultEbtType Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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
