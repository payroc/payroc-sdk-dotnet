using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ContactType>))]
[Serializable]
public readonly record struct ContactType : IStringEnum
{
    public static readonly ContactType Manager = new(Values.Manager);

    public static readonly ContactType Representative = new(Values.Representative);

    public static readonly ContactType Others = new(Values.Others);

    public ContactType(string value)
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
    public static ContactType FromCustom(string value)
    {
        return new ContactType(value);
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

    public static bool operator ==(ContactType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ContactType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ContactType value) => value.Value;

    public static explicit operator ContactType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Manager = "manager";

        public const string Representative = "representative";

        public const string Others = "others";
    }
}
