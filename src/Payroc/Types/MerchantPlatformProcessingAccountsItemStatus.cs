using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(MerchantPlatformProcessingAccountsItemStatus.MerchantPlatformProcessingAccountsItemStatusSerializer)
)]
[Serializable]
public readonly record struct MerchantPlatformProcessingAccountsItemStatus : IStringEnum
{
    public static readonly MerchantPlatformProcessingAccountsItemStatus Entered = new(
        Values.Entered
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Pending = new(
        Values.Pending
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Approved = new(
        Values.Approved
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus SubjectTo = new(
        Values.SubjectTo
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Dormant = new(
        Values.Dormant
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus NonProcessing = new(
        Values.NonProcessing
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Rejected = new(
        Values.Rejected
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Terminated = new(
        Values.Terminated
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Cancelled = new(
        Values.Cancelled
    );

    public static readonly MerchantPlatformProcessingAccountsItemStatus Failed = new(Values.Failed);

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
    public static MerchantPlatformProcessingAccountsItemStatus FromCustom(string value)
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

    public static explicit operator string(MerchantPlatformProcessingAccountsItemStatus value) =>
        value.Value;

    public static explicit operator MerchantPlatformProcessingAccountsItemStatus(string value) =>
        new(value);

    internal class MerchantPlatformProcessingAccountsItemStatusSerializer
        : JsonConverter<MerchantPlatformProcessingAccountsItemStatus>
    {
        public override MerchantPlatformProcessingAccountsItemStatus Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new MerchantPlatformProcessingAccountsItemStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MerchantPlatformProcessingAccountsItemStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MerchantPlatformProcessingAccountsItemStatus ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new MerchantPlatformProcessingAccountsItemStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MerchantPlatformProcessingAccountsItemStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
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
