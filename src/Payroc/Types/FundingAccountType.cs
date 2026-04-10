using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(FundingAccountType.FundingAccountTypeSerializer))]
[Serializable]
public readonly record struct FundingAccountType : IStringEnum
{
    public static readonly FundingAccountType Checking = new(Values.Checking);

    public static readonly FundingAccountType Savings = new(Values.Savings);

    public static readonly FundingAccountType GeneralLedger = new(Values.GeneralLedger);

    public FundingAccountType(string value)
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
    public static FundingAccountType FromCustom(string value)
    {
        return new FundingAccountType(value);
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

    public static bool operator ==(FundingAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(FundingAccountType value) => value.Value;

    public static explicit operator FundingAccountType(string value) => new(value);

    internal class FundingAccountTypeSerializer : JsonConverter<FundingAccountType>
    {
        public override FundingAccountType Read(
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
            return new FundingAccountType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            FundingAccountType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override FundingAccountType ReadAsPropertyName(
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
            return new FundingAccountType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            FundingAccountType value,
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
        public const string Checking = "checking";

        public const string Savings = "savings";

        public const string GeneralLedger = "generalLedger";
    }
}
