using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanOnDelete>))]
public readonly record struct PaymentPlanOnDelete : IStringEnum
{
    public static readonly PaymentPlanOnDelete Complete = new(Values.Complete);

    public static readonly PaymentPlanOnDelete Continue = new(Values.Continue);

    public PaymentPlanOnDelete(string value)
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
    public static PaymentPlanOnDelete FromCustom(string value)
    {
        return new PaymentPlanOnDelete(value);
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

    public static bool operator ==(PaymentPlanOnDelete value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanOnDelete value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanOnDelete value) => value.Value;

    public static explicit operator PaymentPlanOnDelete(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Complete = "complete";

        public const string Continue = "continue";
    }
}
