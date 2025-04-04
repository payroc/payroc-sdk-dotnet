using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<MerchantPlatformProcessingAccountsItemStatus>))]
public readonly record struct MerchantPlatformProcessingAccountsItemStatus : IStringEnum
{
    public static readonly MerchantPlatformProcessingAccountsItemStatus Entered = Custom(
        Values.Entered
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Pending = Custom(
        Values.Pending
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Approved = Custom(
        Values.Approved
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus SubjectTo = Custom(
        Values.SubjectTo
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Dormant = Custom(
        Values.Dormant
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus NonProcessing = Custom(
        Values.NonProcessing
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Rejected = Custom(
        Values.Rejected
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Terminated = Custom(
        Values.Terminated
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Cancelled = Custom(
        Values.Cancelled
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Failed = Custom(
        Values.Failed
    );

    public MerchantPlatformProcessingAccountsItemStatus(string value)
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
    public static MerchantPlatformProcessingAccountsItemStatus Custom(string value)
    {
        return new MerchantPlatformProcessingAccountsItemStatus(value);
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
        MerchantPlatformProcessingAccountsItemStatus value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        MerchantPlatformProcessingAccountsItemStatus value1,
        string value2
    ) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Entered = "entered";

        public const string Pending = "pending";

        public const string Approved = "approved";

        public const string SubjectTo = "subjectTo";

        public const string Dormant = "dormant";

        public const string NonProcessing = "nonProcessing";

        public const string Rejected = "rejected";

        public const string Terminated = "terminated";

        public const string Cancelled = "cancelled";

        public const string Failed = "failed";
    }
}
