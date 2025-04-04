using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CommonFundingFundingSchedule>))]
public readonly record struct CommonFundingFundingSchedule : IStringEnum
{
    public static readonly CommonFundingFundingSchedule Standard = Custom(Values.Standard);

    public static readonly CommonFundingFundingSchedule Nextday = Custom(Values.Nextday);

    public static readonly CommonFundingFundingSchedule Sameday = Custom(Values.Sameday);

    public CommonFundingFundingSchedule(string value)
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
    public static CommonFundingFundingSchedule Custom(string value)
    {
        return new CommonFundingFundingSchedule(value);
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

    public static bool operator ==(CommonFundingFundingSchedule value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CommonFundingFundingSchedule value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Standard = "standard";

        public const string Nextday = "nextday";

        public const string Sameday = "sameday";
    }
}
