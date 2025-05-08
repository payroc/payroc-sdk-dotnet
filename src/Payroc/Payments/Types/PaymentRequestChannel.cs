using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<PaymentRequestChannel>))]
public readonly record struct PaymentRequestChannel : IStringEnum
{
    public static readonly PaymentRequestChannel Pos = new(Values.Pos);

    public static readonly PaymentRequestChannel Web = new(Values.Web);

    public static readonly PaymentRequestChannel Moto = new(Values.Moto);

    public PaymentRequestChannel(string value)
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
    public static PaymentRequestChannel FromCustom(string value)
    {
        return new PaymentRequestChannel(value);
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

    public static bool operator ==(PaymentRequestChannel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PaymentRequestChannel value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PaymentRequestChannel value) => value.Value;

    public static explicit operator PaymentRequestChannel(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Pos = "pos";

        public const string Web = "web";

        public const string Moto = "moto";
    }
}
