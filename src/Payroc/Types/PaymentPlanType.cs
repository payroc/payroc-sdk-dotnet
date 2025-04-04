using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanType>))]
public readonly record struct PaymentPlanType : IStringEnum
{
    public static readonly PaymentPlanType Manual = Custom(Values.Manual);

    public static readonly PaymentPlanType Automatic = Custom(Values.Automatic);

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
    public static PaymentPlanType Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Manual = "manual";

        public const string Automatic = "automatic";
    }
}
