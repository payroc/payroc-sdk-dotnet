using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PricingAgreementUs40Country>))]
[Serializable]
public readonly record struct PricingAgreementUs40Country : IStringEnum
{
    public static readonly PricingAgreementUs40Country Us = new(Values.Us);

    public PricingAgreementUs40Country(string value)
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
    public static PricingAgreementUs40Country FromCustom(string value)
    {
        return new PricingAgreementUs40Country(value);
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

    public static bool operator ==(PricingAgreementUs40Country value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PricingAgreementUs40Country value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PricingAgreementUs40Country value) => value.Value;

    public static explicit operator PricingAgreementUs40Country(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Us = "US";
    }
}
