using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DisputeDisputeType>))]
public readonly record struct DisputeDisputeType : IStringEnum
{
    public static readonly DisputeDisputeType Prearbitration = Custom(Values.Prearbitration);

    public static readonly DisputeDisputeType IssuerReversal = Custom(Values.IssuerReversal);

    public static readonly DisputeDisputeType FirstDisputeWithReversal = Custom(
        Values.FirstDisputeWithReversal
    );

    public static readonly DisputeDisputeType FirstDispute = Custom(Values.FirstDispute);

    public DisputeDisputeType(string value)
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
    public static DisputeDisputeType Custom(string value)
    {
        return new DisputeDisputeType(value);
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

    public static bool operator ==(DisputeDisputeType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DisputeDisputeType value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Prearbitration = "prearbitration";

        public const string IssuerReversal = "issuerReversal";

        public const string FirstDisputeWithReversal = "firstDisputeWithReversal";

        public const string FirstDispute = "firstDispute";
    }
}
