using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<MultiUsePaymentLinkAuthType>))]
public readonly record struct MultiUsePaymentLinkAuthType : IStringEnum
{
    public static readonly MultiUsePaymentLinkAuthType Sale = new(Values.Sale);

    public static readonly MultiUsePaymentLinkAuthType PreAuthorization = new(
        Values.PreAuthorization
    );

    public MultiUsePaymentLinkAuthType(string value)
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
    public static MultiUsePaymentLinkAuthType FromCustom(string value)
    {
        return new MultiUsePaymentLinkAuthType(value);
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

    public static bool operator ==(MultiUsePaymentLinkAuthType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MultiUsePaymentLinkAuthType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MultiUsePaymentLinkAuthType value) => value.Value;

    public static explicit operator MultiUsePaymentLinkAuthType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Sale = "sale";

        public const string PreAuthorization = "preAuthorization";
    }
}
