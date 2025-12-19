using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<AddressTypeType>))]
[Serializable]
public readonly record struct AddressTypeType : IStringEnum
{
    public static readonly AddressTypeType LegalAddress = new(Values.LegalAddress);

    public AddressTypeType(string value)
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
    public static AddressTypeType FromCustom(string value)
    {
        return new AddressTypeType(value);
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

    public static bool operator ==(AddressTypeType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AddressTypeType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AddressTypeType value) => value.Value;

    public static explicit operator AddressTypeType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string LegalAddress = "legalAddress";
    }
}
