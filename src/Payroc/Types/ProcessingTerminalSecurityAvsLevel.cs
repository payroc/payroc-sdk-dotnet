using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingTerminalSecurityAvsLevel>))]
[Serializable]
public readonly record struct ProcessingTerminalSecurityAvsLevel : IStringEnum
{
    public static readonly ProcessingTerminalSecurityAvsLevel FullAddress = new(Values.FullAddress);

    public static readonly ProcessingTerminalSecurityAvsLevel PostalCode = new(Values.PostalCode);

    public ProcessingTerminalSecurityAvsLevel(string value)
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
    public static ProcessingTerminalSecurityAvsLevel FromCustom(string value)
    {
        return new ProcessingTerminalSecurityAvsLevel(value);
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

    public static bool operator ==(ProcessingTerminalSecurityAvsLevel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingTerminalSecurityAvsLevel value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingTerminalSecurityAvsLevel value) => value.Value;

    public static explicit operator ProcessingTerminalSecurityAvsLevel(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string FullAddress = "fullAddress";

        public const string PostalCode = "postalCode";
    }
}
