using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<StandingInstructionsSequence>))]
public readonly record struct StandingInstructionsSequence : IStringEnum
{
    public static readonly StandingInstructionsSequence First = Custom(Values.First);

    public static readonly StandingInstructionsSequence Subsequent = Custom(Values.Subsequent);

    public StandingInstructionsSequence(string value)
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
    public static StandingInstructionsSequence Custom(string value)
    {
        return new StandingInstructionsSequence(value);
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

    public static bool operator ==(StandingInstructionsSequence value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StandingInstructionsSequence value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string First = "first";

        public const string Subsequent = "subsequent";
    }
}
