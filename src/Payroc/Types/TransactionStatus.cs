using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionStatus>))]
[Serializable]
public readonly record struct TransactionStatus : IStringEnum
{
    public static readonly TransactionStatus FullSuspense = new(Values.FullSuspense);

    public static readonly TransactionStatus HeldAudited = new(Values.HeldAudited);

    public static readonly TransactionStatus HeldReleasedAudited = new(Values.HeldReleasedAudited);

    public static readonly TransactionStatus HoldForSettlement30Days = new(
        Values.HoldForSettlement30Days
    );

    public static readonly TransactionStatus HoldForSettlementDuplicate = new(
        Values.HoldForSettlementDuplicate
    );

    public static readonly TransactionStatus HoldLongTerm = new(Values.HoldLongTerm);

    public static readonly TransactionStatus Paid = new(Values.Paid);

    public static readonly TransactionStatus PaidByThirdParty = new(Values.PaidByThirdParty);

    public static readonly TransactionStatus PartialRelease = new(Values.PartialRelease);

    public static readonly TransactionStatus Pull = new(Values.Pull);

    public static readonly TransactionStatus Release = new(Values.Release);

    public static readonly TransactionStatus New = new(Values.New);

    public static readonly TransactionStatus Held = new(Values.Held);

    public static readonly TransactionStatus Unknown = new(Values.Unknown);

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
    public static TransactionStatus FromCustom(string value)
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

    public static explicit operator string(TransactionStatus value) => value.Value;

    public static explicit operator TransactionStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
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
