using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TerminalOrderTrainingProvider>))]
public readonly record struct TerminalOrderTrainingProvider : IStringEnum
{
    public static readonly TerminalOrderTrainingProvider Partner = new(Values.Partner);

    public static readonly TerminalOrderTrainingProvider Payroc = new(Values.Payroc);

    public TerminalOrderTrainingProvider(string value)
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
    public static TerminalOrderTrainingProvider FromCustom(string value)
    {
        return new TerminalOrderTrainingProvider(value);
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

    public static bool operator ==(TerminalOrderTrainingProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TerminalOrderTrainingProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TerminalOrderTrainingProvider value) => value.Value;

    public static explicit operator TerminalOrderTrainingProvider(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Partner = "partner";

        public const string Payroc = "payroc";
    }
}
