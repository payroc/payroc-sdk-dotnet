using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<StandingInstructionsProcessingModel>))]
public readonly record struct StandingInstructionsProcessingModel : IStringEnum
{
    public static readonly StandingInstructionsProcessingModel Unscheduled = Custom(
        Values.Unscheduled
    );

    public static readonly StandingInstructionsProcessingModel Recurring = Custom(Values.Recurring);

    public static readonly StandingInstructionsProcessingModel Installment = Custom(
        Values.Installment
    );

    public StandingInstructionsProcessingModel(string value)
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
    public static StandingInstructionsProcessingModel Custom(string value)
    {
        return new StandingInstructionsProcessingModel(value);
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

    public static bool operator ==(StandingInstructionsProcessingModel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(StandingInstructionsProcessingModel value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Unscheduled = "unscheduled";

        public const string Recurring = "recurring";

        public const string Installment = "installment";
    }
}
