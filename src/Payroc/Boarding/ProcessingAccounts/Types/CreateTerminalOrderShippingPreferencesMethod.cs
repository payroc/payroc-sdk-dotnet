using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Boarding.ProcessingAccounts;

[JsonConverter(typeof(StringEnumSerializer<CreateTerminalOrderShippingPreferencesMethod>))]
public readonly record struct CreateTerminalOrderShippingPreferencesMethod : IStringEnum
{
    public static readonly CreateTerminalOrderShippingPreferencesMethod NextDay = new(
        Values.NextDay
    );

    public static readonly CreateTerminalOrderShippingPreferencesMethod Ground = new(Values.Ground);

    public CreateTerminalOrderShippingPreferencesMethod(string value)
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
    public static CreateTerminalOrderShippingPreferencesMethod FromCustom(string value)
    {
        return new CreateTerminalOrderShippingPreferencesMethod(value);
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
        CreateTerminalOrderShippingPreferencesMethod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateTerminalOrderShippingPreferencesMethod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateTerminalOrderShippingPreferencesMethod value) =>
        value.Value;

    public static explicit operator CreateTerminalOrderShippingPreferencesMethod(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string NextDay = "nextDay";

        public const string Ground = "ground";
    }
}
