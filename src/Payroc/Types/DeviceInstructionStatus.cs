using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DeviceInstructionStatus>))]
public readonly record struct DeviceInstructionStatus : IStringEnum
{
    public static readonly DeviceInstructionStatus Canceled = new(Values.Canceled);

    public static readonly DeviceInstructionStatus Completed = new(Values.Completed);

    public static readonly DeviceInstructionStatus Failure = new(Values.Failure);

    public static readonly DeviceInstructionStatus InProgress = new(Values.InProgress);

    public DeviceInstructionStatus(string value)
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
    public static DeviceInstructionStatus FromCustom(string value)
    {
        return new DeviceInstructionStatus(value);
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

    public static bool operator ==(DeviceInstructionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DeviceInstructionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DeviceInstructionStatus value) => value.Value;

    public static explicit operator DeviceInstructionStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Canceled = "canceled";

        public const string Completed = "completed";

        public const string Failure = "failure";

        public const string InProgress = "inProgress";
    }
}
