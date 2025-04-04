using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RewardPayChoiceFeesDebitOption>))]
public readonly record struct RewardPayChoiceFeesDebitOption : IStringEnum
{
    public static readonly RewardPayChoiceFeesDebitOption InterchangePlus = Custom(
        Values.InterchangePlus
    );

    public static readonly RewardPayChoiceFeesDebitOption FlatRate = Custom(Values.FlatRate);

    public RewardPayChoiceFeesDebitOption(string value)
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
    public static RewardPayChoiceFeesDebitOption Custom(string value)
    {
        return new RewardPayChoiceFeesDebitOption(value);
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

    public static bool operator ==(RewardPayChoiceFeesDebitOption value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RewardPayChoiceFeesDebitOption value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string InterchangePlus = "interchangePlus";

        public const string FlatRate = "flatRate";
    }
}
