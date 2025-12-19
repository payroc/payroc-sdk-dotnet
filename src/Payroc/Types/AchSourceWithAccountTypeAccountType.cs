using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<AchSourceWithAccountTypeAccountType>))]
[Serializable]
public readonly record struct AchSourceWithAccountTypeAccountType : IStringEnum
{
    public static readonly AchSourceWithAccountTypeAccountType Checking = new(Values.Checking);

    public static readonly AchSourceWithAccountTypeAccountType Savings = new(Values.Savings);

    public AchSourceWithAccountTypeAccountType(string value)
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
    public static AchSourceWithAccountTypeAccountType FromCustom(string value)
    {
        return new AchSourceWithAccountTypeAccountType(value);
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

    public static bool operator ==(AchSourceWithAccountTypeAccountType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AchSourceWithAccountTypeAccountType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AchSourceWithAccountTypeAccountType value) =>
        value.Value;

    public static explicit operator AchSourceWithAccountTypeAccountType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Checking = "checking";

        public const string Savings = "savings";
    }
}
