using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RewardPayFeesTips>))]
[Serializable]
public readonly record struct RewardPayFeesTips : IStringEnum
{
    public static readonly RewardPayFeesTips NoTips = new(Values.NoTips);

    public static readonly RewardPayFeesTips Prompt = new(Values.Prompt);

    public static readonly RewardPayFeesTips TipAdjust = new(Values.TipAdjust);

    public RewardPayFeesTips(string value)
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
    public static RewardPayFeesTips FromCustom(string value)
    {
        return new RewardPayFeesTips(value);
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

    public static bool operator ==(RewardPayFeesTips value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RewardPayFeesTips value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RewardPayFeesTips value) => value.Value;

    public static explicit operator RewardPayFeesTips(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string NoTips = "noTips";

        public const string Prompt = "prompt";

        public const string TipAdjust = "tipAdjust";
    }
}
