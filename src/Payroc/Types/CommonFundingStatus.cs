using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CommonFundingStatus>))]
public readonly record struct CommonFundingStatus : IStringEnum
{
    public static readonly CommonFundingStatus Enabled = Custom(Values.Enabled);

    public static readonly CommonFundingStatus Disabled = Custom(Values.Disabled);

    public CommonFundingStatus(string value)
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
    public static CommonFundingStatus Custom(string value)
    {
        return new CommonFundingStatus(value);
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

    public static bool operator ==(CommonFundingStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CommonFundingStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Enabled = "enabled";

        public const string Disabled = "disabled";
    }
}
