using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanType>))]
[Serializable]
public readonly record struct PaymentPlanType : IStringEnum
{
    public static readonly PaymentPlanType Manual = new(Values.Manual);

    public static readonly PaymentPlanType Automatic = new(Values.Automatic);

    public PaymentPlanType(string value)
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
    public static PaymentPlanType FromCustom(string value)
    {
        return new PaymentPlanType(value);
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

    public static bool operator ==(PaymentPlanType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanType value) => value.Value;

    public static explicit operator PaymentPlanType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Manual = "manual";

        public const string Automatic = "automatic";
    }
}
