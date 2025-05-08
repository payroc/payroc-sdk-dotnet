using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RewardPayChoiceFeesCreditTips>))]
public readonly record struct RewardPayChoiceFeesCreditTips : IStringEnum
{
    public static readonly RewardPayChoiceFeesCreditTips NoTips = new(Values.NoTips);

    public static readonly RewardPayChoiceFeesCreditTips Prompt = new(Values.Prompt);

    public static readonly RewardPayChoiceFeesCreditTips TipAdjust = new(Values.TipAdjust);

    public RewardPayChoiceFeesCreditTips(string value)
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
    public static RewardPayChoiceFeesCreditTips FromCustom(string value)
    {
        return new RewardPayChoiceFeesCreditTips(value);
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

    public static bool operator ==(RewardPayChoiceFeesCreditTips value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RewardPayChoiceFeesCreditTips value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RewardPayChoiceFeesCreditTips value) => value.Value;

    public static explicit operator RewardPayChoiceFeesCreditTips(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string NoTips = "noTips";

        public const string Prompt = "prompt";

        public const string TipAdjust = "tipAdjust";
    }
}
