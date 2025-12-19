using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BusinessCountryOfOperation>))]
[Serializable]
public readonly record struct BusinessCountryOfOperation : IStringEnum
{
    public static readonly BusinessCountryOfOperation Us = new(Values.Us);

    public BusinessCountryOfOperation(string value)
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
    public static BusinessCountryOfOperation FromCustom(string value)
    {
        return new BusinessCountryOfOperation(value);
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

    public static bool operator ==(BusinessCountryOfOperation value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(BusinessCountryOfOperation value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(BusinessCountryOfOperation value) => value.Value;

    public static explicit operator BusinessCountryOfOperation(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Us = "US";
    }
}
