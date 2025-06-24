using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BaseIntentStatus>))]
[Serializable]
public readonly record struct BaseIntentStatus : IStringEnum
{
    public static readonly BaseIntentStatus Active = new(Values.Active);

    public static readonly BaseIntentStatus PendingReview = new(Values.PendingReview);

    public static readonly BaseIntentStatus Rejected = new(Values.Rejected);

    public BaseIntentStatus(string value)
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
    public static BaseIntentStatus FromCustom(string value)
    {
        return new BaseIntentStatus(value);
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

    public static bool operator ==(BaseIntentStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BaseIntentStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BaseIntentStatus value) => value.Value;

    public static explicit operator BaseIntentStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Active = "active";

        public const string PendingReview = "pendingReview";

        public const string Rejected = "rejected";
    }
}
