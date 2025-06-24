using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<SingleUsePaymentLinkStatus>))]
[Serializable]
public readonly record struct SingleUsePaymentLinkStatus : IStringEnum
{
    public static readonly SingleUsePaymentLinkStatus Active = new(Values.Active);

    public static readonly SingleUsePaymentLinkStatus Completed = new(Values.Completed);

    public static readonly SingleUsePaymentLinkStatus Deactivated = new(Values.Deactivated);

    public static readonly SingleUsePaymentLinkStatus Expired = new(Values.Expired);

    public SingleUsePaymentLinkStatus(string value)
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
    public static SingleUsePaymentLinkStatus FromCustom(string value)
    {
        return new SingleUsePaymentLinkStatus(value);
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

    public static bool operator ==(SingleUsePaymentLinkStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(SingleUsePaymentLinkStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(SingleUsePaymentLinkStatus value) => value.Value;

    public static explicit operator SingleUsePaymentLinkStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Active = "active";

        public const string Completed = "completed";

        public const string Deactivated = "deactivated";

        public const string Expired = "expired";
    }
}
