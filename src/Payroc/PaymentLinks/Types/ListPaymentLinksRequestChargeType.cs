using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentLinks;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentLinksRequestChargeType>))]
[Serializable]
public readonly record struct ListPaymentLinksRequestChargeType : IStringEnum
{
    public static readonly ListPaymentLinksRequestChargeType Preset = new(Values.Preset);

    public static readonly ListPaymentLinksRequestChargeType Prompt = new(Values.Prompt);

    public ListPaymentLinksRequestChargeType(string value)
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
    public static ListPaymentLinksRequestChargeType FromCustom(string value)
    {
        return new ListPaymentLinksRequestChargeType(value);
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

    public static bool operator ==(ListPaymentLinksRequestChargeType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentLinksRequestChargeType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPaymentLinksRequestChargeType value) => value.Value;

    public static explicit operator ListPaymentLinksRequestChargeType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Preset = "preset";

        public const string Prompt = "prompt";
    }
}
