using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionStatus>))]
public readonly record struct TransactionStatus : IStringEnum
{
    public static readonly TransactionStatus FullSuspense = Custom(Values.FullSuspense);

    public static readonly TransactionStatus HeldAudited = Custom(Values.HeldAudited);

    public static readonly TransactionStatus HeldReleasedAudited = Custom(
        Values.HeldReleasedAudited
    );

    public static readonly TransactionStatus HoldForSettlement30Days = Custom(
        Values.HoldForSettlement30Days
    );

    public static readonly TransactionStatus HoldForSettlementDuplicate = Custom(
        Values.HoldForSettlementDuplicate
    );

    public static readonly TransactionStatus HoldLongTerm = Custom(Values.HoldLongTerm);

    public static readonly TransactionStatus Paid = Custom(Values.Paid);

    public static readonly TransactionStatus PaidByThirdParty = Custom(Values.PaidByThirdParty);

    public static readonly TransactionStatus PartialRelease = Custom(Values.PartialRelease);

    public static readonly TransactionStatus Pull = Custom(Values.Pull);

    public static readonly TransactionStatus Release = Custom(Values.Release);

    public static readonly TransactionStatus New = Custom(Values.New);

    public static readonly TransactionStatus Held = Custom(Values.Held);

    public static readonly TransactionStatus Unknown = Custom(Values.Unknown);

    public TransactionStatus(string value)
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
    public static TransactionStatus Custom(string value)
    {
        return new TransactionStatus(value);
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

    public static bool operator ==(TransactionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string FullSuspense = "fullSuspense";

        public const string HeldAudited = "heldAudited";

        public const string HeldReleasedAudited = "heldReleasedAudited";

        public const string HoldForSettlement30Days = "holdForSettlement30Days";

        public const string HoldForSettlementDuplicate = "holdForSettlementDuplicate";

        public const string HoldLongTerm = "holdLongTerm";

        public const string Paid = "paid";

        public const string PaidByThirdParty = "paidByThirdParty";

        public const string PartialRelease = "partialRelease";

        public const string Pull = "pull";

        public const string Release = "release";

        public const string New = "new";

        public const string Held = "held";

        public const string Unknown = "unknown";
    }
}
