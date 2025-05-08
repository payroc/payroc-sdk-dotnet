using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingAccountStatus>))]
public readonly record struct ProcessingAccountStatus : IStringEnum
{
    public static readonly ProcessingAccountStatus Entered = new(Values.Entered);

    public static readonly ProcessingAccountStatus Pending = new(Values.Pending);

    public static readonly ProcessingAccountStatus Approved = new(Values.Approved);

    public static readonly ProcessingAccountStatus SubjectTo = new(Values.SubjectTo);

    public static readonly ProcessingAccountStatus Dormant = new(Values.Dormant);

    public static readonly ProcessingAccountStatus NonProcessing = new(Values.NonProcessing);

    public static readonly ProcessingAccountStatus Rejected = new(Values.Rejected);

    public static readonly ProcessingAccountStatus Terminated = new(Values.Terminated);

    public static readonly ProcessingAccountStatus Cancelled = new(Values.Cancelled);

    public ProcessingAccountStatus(string value)
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
    public static ProcessingAccountStatus FromCustom(string value)
    {
        return new ProcessingAccountStatus(value);
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

    public static bool operator ==(ProcessingAccountStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingAccountStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingAccountStatus value) => value.Value;

    public static explicit operator ProcessingAccountStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Entered = "entered";

        public const string Pending = "pending";

        public const string Approved = "approved";

        public const string SubjectTo = "subjectTo";

        public const string Dormant = "dormant";

        public const string NonProcessing = "nonProcessing";

        public const string Rejected = "rejected";

        public const string Terminated = "terminated";

        public const string Cancelled = "cancelled";
    }
}
