using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanFrequency>))]
[Serializable]
public readonly record struct PaymentPlanFrequency : IStringEnum
{
    public static readonly PaymentPlanFrequency Weekly = new(Values.Weekly);

    public static readonly PaymentPlanFrequency Fortnightly = new(Values.Fortnightly);

    public static readonly PaymentPlanFrequency Monthly = new(Values.Monthly);

    public static readonly PaymentPlanFrequency Quarterly = new(Values.Quarterly);

    public static readonly PaymentPlanFrequency Yearly = new(Values.Yearly);

    public PaymentPlanFrequency(string value)
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
    public static PaymentPlanFrequency FromCustom(string value)
    {
        return new PaymentPlanFrequency(value);
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

    public static bool operator ==(PaymentPlanFrequency value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanFrequency value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanFrequency value) => value.Value;

    public static explicit operator PaymentPlanFrequency(string value) => new(value);

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
