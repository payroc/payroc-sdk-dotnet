using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanBaseFrequency>))]
[Serializable]
public readonly record struct PaymentPlanBaseFrequency : IStringEnum
{
    public static readonly PaymentPlanBaseFrequency Weekly = new(Values.Weekly);

    public static readonly PaymentPlanBaseFrequency Fortnightly = new(Values.Fortnightly);

    public static readonly PaymentPlanBaseFrequency Monthly = new(Values.Monthly);

    public static readonly PaymentPlanBaseFrequency Quarterly = new(Values.Quarterly);

    public static readonly PaymentPlanBaseFrequency Yearly = new(Values.Yearly);

    public PaymentPlanBaseFrequency(string value)
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
    public static PaymentPlanBaseFrequency FromCustom(string value)
    {
        return new PaymentPlanBaseFrequency(value);
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

    public static bool operator ==(PaymentPlanBaseFrequency value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanBaseFrequency value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanBaseFrequency value) => value.Value;

    public static explicit operator PaymentPlanBaseFrequency(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Weekly = "weekly";

        public const string Fortnightly = "fortnightly";

        public const string Monthly = "monthly";

        public const string Quarterly = "quarterly";

        public const string Yearly = "yearly";
    }
}
