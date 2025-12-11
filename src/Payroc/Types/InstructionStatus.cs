using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<InstructionStatus>))]
[Serializable]
public readonly record struct InstructionStatus : IStringEnum
{
    public static readonly InstructionStatus Accepted = new(Values.Accepted);

    public static readonly InstructionStatus Pending = new(Values.Pending);

    public static readonly InstructionStatus Completed = new(Values.Completed);

    public InstructionStatus(string value)
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
    public static InstructionStatus FromCustom(string value)
    {
        return new InstructionStatus(value);
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

    public static bool operator ==(InstructionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InstructionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InstructionStatus value) => value.Value;

    public static explicit operator InstructionStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Accepted = "accepted";

        public const string Pending = "pending";

        public const string Completed = "completed";
    }
}
