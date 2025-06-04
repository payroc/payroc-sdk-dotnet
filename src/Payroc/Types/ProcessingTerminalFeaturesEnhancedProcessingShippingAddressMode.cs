using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(StringEnumSerializer<ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode>)
)]
public readonly record struct ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode
    : IStringEnum
{
    public static readonly ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode FullAddress =
        new(Values.FullAddress);

    public static readonly ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode PostalCode =
        new(Values.PostalCode);

    public ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode(string value)
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
    public static ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode FromCustom(
        string value
    )
    {
        return new ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode(value);
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
        ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode value
    ) => value.Value;

    public static explicit operator ProcessingTerminalFeaturesEnhancedProcessingShippingAddressMode(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string FullAddress = "fullAddress";

        public const string PostalCode = "postalCode";
    }
}
