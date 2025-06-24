using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TrainingProvider>))]
[Serializable]
public readonly record struct TrainingProvider : IStringEnum
{
    public static readonly TrainingProvider Partner = new(Values.Partner);

    public static readonly TrainingProvider Payroc = new(Values.Payroc);

    public TrainingProvider(string value)
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
    public static TrainingProvider FromCustom(string value)
    {
        return new TrainingProvider(value);
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

    public static bool operator ==(TrainingProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TrainingProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TrainingProvider value) => value.Value;

    public static explicit operator TrainingProvider(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Partner = "partner";

        public const string Payroc = "payroc";
    }
}
