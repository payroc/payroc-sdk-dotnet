using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<MultiUsePaymentLinkStatus>))]
public readonly record struct MultiUsePaymentLinkStatus : IStringEnum
{
    public static readonly MultiUsePaymentLinkStatus Active = new(Values.Active);

    public static readonly MultiUsePaymentLinkStatus Completed = new(Values.Completed);

    public static readonly MultiUsePaymentLinkStatus Deactivated = new(Values.Deactivated);

    public static readonly MultiUsePaymentLinkStatus Expired = new(Values.Expired);

    public MultiUsePaymentLinkStatus(string value)
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
    public static MultiUsePaymentLinkStatus FromCustom(string value)
    {
        return new MultiUsePaymentLinkStatus(value);
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

    public static bool operator ==(MultiUsePaymentLinkStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MultiUsePaymentLinkStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MultiUsePaymentLinkStatus value) => value.Value;

    public static explicit operator MultiUsePaymentLinkStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Active = "active";

        public const string Completed = "completed";

        public const string Deactivated = "deactivated";

        public const string Expired = "expired";
    }
}
