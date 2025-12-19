using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentLinks;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentLinksRequestLinkType>))]
[Serializable]
public readonly record struct ListPaymentLinksRequestLinkType : IStringEnum
{
    public static readonly ListPaymentLinksRequestLinkType MultiUse = new(Values.MultiUse);

    public static readonly ListPaymentLinksRequestLinkType SingleUse = new(Values.SingleUse);

    public ListPaymentLinksRequestLinkType(string value)
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
    public static ListPaymentLinksRequestLinkType FromCustom(string value)
    {
        return new ListPaymentLinksRequestLinkType(value);
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

    public static bool operator ==(ListPaymentLinksRequestLinkType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentLinksRequestLinkType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPaymentLinksRequestLinkType value) => value.Value;

    public static explicit operator ListPaymentLinksRequestLinkType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string MultiUse = "multiUse";

        public const string SingleUse = "singleUse";
    }
}
