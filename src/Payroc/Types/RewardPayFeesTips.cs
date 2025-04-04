using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RewardPayFeesTips>))]
public readonly record struct RewardPayFeesTips : IStringEnum
{
    public static readonly RewardPayFeesTips NoTips = Custom(Values.NoTips);

    public static readonly RewardPayFeesTips Prompt = Custom(Values.Prompt);

    public static readonly RewardPayFeesTips TipAdjust = Custom(Values.TipAdjust);

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
    public static RewardPayFeesTips Custom(string value)
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
