using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TipMode>))]
public readonly record struct TipMode : IStringEnum
{
    public static readonly TipMode Prompted = Custom(Values.Prompted);

    public static readonly TipMode Adjusted = Custom(Values.Adjusted);

    public TipMode(string value)
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
    public static TipMode Custom(string value)
    {
        return new TipMode(value);
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

    public static bool operator ==(TipMode value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(TipMode value1, string value2) => !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Prompted = "prompted";

        public const string Adjusted = "adjusted";
    }
}
