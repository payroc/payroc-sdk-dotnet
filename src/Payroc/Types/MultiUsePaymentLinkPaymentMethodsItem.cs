using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<MultiUsePaymentLinkPaymentMethodsItem>))]
[Serializable]
public readonly record struct MultiUsePaymentLinkPaymentMethodsItem : IStringEnum
{
    public static readonly MultiUsePaymentLinkPaymentMethodsItem Card = new(Values.Card);

    public static readonly MultiUsePaymentLinkPaymentMethodsItem BankTransfer = new(
        Values.BankTransfer
    );

    public MultiUsePaymentLinkPaymentMethodsItem(string value)
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
    public static MultiUsePaymentLinkPaymentMethodsItem FromCustom(string value)
    {
        return new MultiUsePaymentLinkPaymentMethodsItem(value);
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

    public static bool operator ==(MultiUsePaymentLinkPaymentMethodsItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MultiUsePaymentLinkPaymentMethodsItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MultiUsePaymentLinkPaymentMethodsItem value) =>
        value.Value;

    public static explicit operator MultiUsePaymentLinkPaymentMethodsItem(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Card = "card";

        public const string BankTransfer = "bankTransfer";
    }
}
