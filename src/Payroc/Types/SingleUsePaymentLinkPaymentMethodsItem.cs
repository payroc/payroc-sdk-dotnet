using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SingleUsePaymentLinkPaymentMethodsItem>))]
[Serializable]
public readonly record struct SingleUsePaymentLinkPaymentMethodsItem : IStringEnum
{
    public static readonly SingleUsePaymentLinkPaymentMethodsItem Card = new(Values.Card);

    public static readonly SingleUsePaymentLinkPaymentMethodsItem BankTransfer = new(
        Values.BankTransfer
    );

    public SingleUsePaymentLinkPaymentMethodsItem(string value)
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
    public static SingleUsePaymentLinkPaymentMethodsItem FromCustom(string value)
    {
        return new SingleUsePaymentLinkPaymentMethodsItem(value);
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

    public static bool operator ==(SingleUsePaymentLinkPaymentMethodsItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUsePaymentLinkPaymentMethodsItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SingleUsePaymentLinkPaymentMethodsItem value) =>
        value.Value;

    public static explicit operator SingleUsePaymentLinkPaymentMethodsItem(string value) =>
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
