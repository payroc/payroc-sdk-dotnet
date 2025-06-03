using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SingleUsePaymentLinkAuthType>))]
public readonly record struct SingleUsePaymentLinkAuthType : IStringEnum
{
    public static readonly SingleUsePaymentLinkAuthType Sale = new(Values.Sale);

    public static readonly SingleUsePaymentLinkAuthType PreAuthorization = new(
        Values.PreAuthorization
    );

    public SingleUsePaymentLinkAuthType(string value)
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
    public static SingleUsePaymentLinkAuthType FromCustom(string value)
    {
        return new SingleUsePaymentLinkAuthType(value);
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

    public static bool operator ==(SingleUsePaymentLinkAuthType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUsePaymentLinkAuthType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SingleUsePaymentLinkAuthType value) => value.Value;

    public static explicit operator SingleUsePaymentLinkAuthType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Sale = "sale";

        public const string PreAuthorization = "preAuthorization";
    }
}
