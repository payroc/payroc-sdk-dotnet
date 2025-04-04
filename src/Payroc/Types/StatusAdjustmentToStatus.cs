using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<StatusAdjustmentToStatus>))]
public readonly record struct StatusAdjustmentToStatus : IStringEnum
{
    public static readonly StatusAdjustmentToStatus Ready = Custom(Values.Ready);

    public static readonly StatusAdjustmentToStatus Pending = Custom(Values.Pending);

    public static readonly StatusAdjustmentToStatus Declined = Custom(Values.Declined);

    public StatusAdjustmentToStatus(string value)
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
    public static StatusAdjustmentToStatus Custom(string value)
    {
        return new StatusAdjustmentToStatus(value);
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

    public static bool operator ==(StatusAdjustmentToStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StatusAdjustmentToStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Ready = "ready";

        public const string Pending = "pending";

        public const string Declined = "declined";
    }
}
