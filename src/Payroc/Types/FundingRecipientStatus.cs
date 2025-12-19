using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<FundingRecipientStatus>))]
[Serializable]
public readonly record struct FundingRecipientStatus : IStringEnum
{
    public static readonly FundingRecipientStatus Approved = new(Values.Approved);

    public static readonly FundingRecipientStatus Rejected = new(Values.Rejected);

    public static readonly FundingRecipientStatus Pending = new(Values.Pending);

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
    public static FundingRecipientStatus FromCustom(string value)
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

    public static explicit operator string(FundingRecipientStatus value) => value.Value;

    public static explicit operator FundingRecipientStatus(string value) => new(value);

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
