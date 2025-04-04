using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<InstructionMerchantsItemRecipientsItemStatus>))]
public readonly record struct InstructionMerchantsItemRecipientsItemStatus : IStringEnum
{
    public static readonly InstructionMerchantsItemRecipientsItemStatus Accepted = Custom(
        Values.Accepted
    );

    public static readonly InstructionMerchantsItemRecipientsItemStatus Pending = Custom(
        Values.Pending
    );

    public static readonly InstructionMerchantsItemRecipientsItemStatus Released = Custom(
        Values.Released
    );

    public static readonly InstructionMerchantsItemRecipientsItemStatus Funded = Custom(
        Values.Funded
    );

    public static readonly InstructionMerchantsItemRecipientsItemStatus Failed = Custom(
        Values.Failed
    );

    public static readonly InstructionMerchantsItemRecipientsItemStatus Rejected = Custom(
        Values.Rejected
    );

    public static readonly InstructionMerchantsItemRecipientsItemStatus OnHold = Custom(
        Values.OnHold
    );

    public InstructionMerchantsItemRecipientsItemStatus(string value)
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
    public static InstructionMerchantsItemRecipientsItemStatus Custom(string value)
    {
        return new InstructionMerchantsItemRecipientsItemStatus(value);
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

    public static bool operator ==(
        InstructionMerchantsItemRecipientsItemStatus value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        InstructionMerchantsItemRecipientsItemStatus value1,
        string value2
    ) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Accepted = "accepted";

        public const string Pending = "pending";

        public const string Released = "released";

        public const string Funded = "funded";

        public const string Failed = "failed";

        public const string Rejected = "rejected";

        public const string OnHold = "onHold";
    }
}
