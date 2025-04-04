using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingAccountStatus>))]
public readonly record struct ProcessingAccountStatus : IStringEnum
{
    public static readonly ProcessingAccountStatus Entered = Custom(Values.Entered);

    public static readonly ProcessingAccountStatus Pending = Custom(Values.Pending);

    public static readonly ProcessingAccountStatus Approved = Custom(Values.Approved);

    public static readonly ProcessingAccountStatus SubjectTo = Custom(Values.SubjectTo);

    public static readonly ProcessingAccountStatus Dormant = Custom(Values.Dormant);

    public static readonly ProcessingAccountStatus NonProcessing = Custom(Values.NonProcessing);

    public static readonly ProcessingAccountStatus Rejected = Custom(Values.Rejected);

    public static readonly ProcessingAccountStatus Terminated = Custom(Values.Terminated);

    public static readonly ProcessingAccountStatus Cancelled = Custom(Values.Cancelled);

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
    public static ProcessingAccountStatus Custom(string value)
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
