using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(FundingAccountSummaryStatus.FundingAccountSummaryStatusSerializer))]
[Serializable]
public readonly record struct FundingAccountSummaryStatus : IStringEnum
{
    public static readonly FundingAccountSummaryStatus Approved = new(Values.Approved);

    public static readonly FundingAccountSummaryStatus Rejected = new(Values.Rejected);

    public static readonly FundingAccountSummaryStatus Pending = new(Values.Pending);

    public FundingAccountSummaryStatus(string value)
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
    public static FundingAccountSummaryStatus FromCustom(string value)
    {
        return new FundingAccountSummaryStatus(value);
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

    public static bool operator ==(FundingAccountSummaryStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingAccountSummaryStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FundingAccountSummaryStatus value) => value.Value;

    public static explicit operator FundingAccountSummaryStatus(string value) => new(value);

    internal class FundingAccountSummaryStatusSerializer
        : JsonConverter<FundingAccountSummaryStatus>
    {
        public override FundingAccountSummaryStatus Read(
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
            return new FundingAccountSummaryStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FundingAccountSummaryStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override FundingAccountSummaryStatus ReadAsPropertyName(
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
            return new FundingAccountSummaryStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            FundingAccountSummaryStatus value,
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
        public const string Approved = "approved";

        public const string Rejected = "rejected";

        public const string Pending = "pending";
    }
}
