using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<PaymentLinkEmailShareEventSharingMethod>))]
[Serializable]
public readonly record struct PaymentLinkEmailShareEventSharingMethod : IStringEnum
{
    public static readonly PaymentLinkEmailShareEventSharingMethod Email = new(Values.Email);

    public PaymentLinkEmailShareEventSharingMethod(string value)
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
    public static PaymentLinkEmailShareEventSharingMethod FromCustom(string value)
    {
        return new PaymentLinkEmailShareEventSharingMethod(value);
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

    public static bool operator ==(PaymentLinkEmailShareEventSharingMethod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentLinkEmailShareEventSharingMethod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentLinkEmailShareEventSharingMethod value) =>
        value.Value;

    public static explicit operator PaymentLinkEmailShareEventSharingMethod(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Email = "email";
    }
}
