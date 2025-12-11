using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentPlanBaseOnDelete>))]
[Serializable]
public readonly record struct PaymentPlanBaseOnDelete : IStringEnum
{
    public static readonly PaymentPlanBaseOnDelete Complete = new(Values.Complete);

    public static readonly PaymentPlanBaseOnDelete Continue = new(Values.Continue);

    public PaymentPlanBaseOnDelete(string value)
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
    public static PaymentPlanBaseOnDelete FromCustom(string value)
    {
        return new PaymentPlanBaseOnDelete(value);
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

    public static bool operator ==(PaymentPlanBaseOnDelete value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentPlanBaseOnDelete value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentPlanBaseOnDelete value) => value.Value;

    public static explicit operator PaymentPlanBaseOnDelete(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Complete = "complete";

        public const string Continue = "continue";
    }
}
