using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingRecipientStatus>))]
public readonly record struct FundingRecipientStatus : IStringEnum
{
    public static readonly FundingRecipientStatus Approved = Custom(Values.Approved);

    public static readonly FundingRecipientStatus Rejected = Custom(Values.Rejected);

    public static readonly FundingRecipientStatus Pending = Custom(Values.Pending);

    public FundingRecipientStatus(string value)
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
    public static FundingRecipientStatus Custom(string value)
    {
        return new FundingRecipientStatus(value);
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

    public static bool operator ==(FundingRecipientStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FundingRecipientStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Approved = "approved";

        public const string Rejected = "rejected";

        public const string Pending = "pending";
    }
}
