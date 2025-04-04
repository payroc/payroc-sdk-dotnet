using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.Refunds;

[JsonConverter(typeof(StringEnumSerializer<UnreferencedRefundChannel>))]
public readonly record struct UnreferencedRefundChannel : IStringEnum
{
    public static readonly UnreferencedRefundChannel Pos = Custom(Values.Pos);

    public static readonly UnreferencedRefundChannel Moto = Custom(Values.Moto);

    public UnreferencedRefundChannel(string value)
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
    public static UnreferencedRefundChannel Custom(string value)
    {
        return new UnreferencedRefundChannel(value);
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

    public static bool operator ==(UnreferencedRefundChannel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UnreferencedRefundChannel value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Pos = "pos";

        public const string Moto = "moto";
    }
}
