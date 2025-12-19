using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CustomLabelElement>))]
[Serializable]
public readonly record struct CustomLabelElement : IStringEnum
{
    public static readonly CustomLabelElement PaymentButton = new(Values.PaymentButton);

    public CustomLabelElement(string value)
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
    public static CustomLabelElement FromCustom(string value)
    {
        return new CustomLabelElement(value);
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

    public static bool operator ==(CustomLabelElement value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CustomLabelElement value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CustomLabelElement value) => value.Value;

    public static explicit operator CustomLabelElement(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string PaymentButton = "paymentButton";
    }
}
