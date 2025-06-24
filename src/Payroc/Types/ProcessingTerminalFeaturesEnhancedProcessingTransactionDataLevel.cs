using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(StringEnumSerializer<ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel>)
)]
[Serializable]
public readonly record struct ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel
    : IStringEnum
{
    public static readonly ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel Level2 =
        new(Values.Level2);

    public static readonly ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel Level3 =
        new(Values.Level3);

    public ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel(string value)
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
    public static ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel FromCustom(
        string value
    )
    {
        return new ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel(value);
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

    public static bool operator ==(
        ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel value
    ) => value.Value;

    public static explicit operator ProcessingTerminalFeaturesEnhancedProcessingTransactionDataLevel(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Level2 = "level2";

        public const string Level3 = "level3";
    }
}
