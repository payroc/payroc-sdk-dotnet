using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanOnUpdate>))]
[Serializable]
public readonly record struct PaymentPlanOnUpdate : IStringEnum
{
    public static readonly PaymentPlanOnUpdate Update = new(Values.Update);

    public static readonly PaymentPlanOnUpdate Continue = new(Values.Continue);

    public PaymentPlanOnUpdate(string value)
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
    public static PaymentPlanOnUpdate FromCustom(string value)
    {
        return new PaymentPlanOnUpdate(value);
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

    public static bool operator ==(PaymentPlanOnUpdate value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanOnUpdate value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanOnUpdate value) => value.Value;

    public static explicit operator PaymentPlanOnUpdate(string value) => new(value);

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
