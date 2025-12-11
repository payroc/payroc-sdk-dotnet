using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanBaseOnUpdate>))]
[Serializable]
public readonly record struct PaymentPlanBaseOnUpdate : IStringEnum
{
    public static readonly PaymentPlanBaseOnUpdate Update = new(Values.Update);

    public static readonly PaymentPlanBaseOnUpdate Continue = new(Values.Continue);

    public PaymentPlanBaseOnUpdate(string value)
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
    public static PaymentPlanBaseOnUpdate FromCustom(string value)
    {
        return new PaymentPlanBaseOnUpdate(value);
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

    public static bool operator ==(PaymentPlanBaseOnUpdate value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanBaseOnUpdate value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanBaseOnUpdate value) => value.Value;

    public static explicit operator PaymentPlanBaseOnUpdate(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Update = "update";

        public const string Continue = "continue";
    }
}
