using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<TransactionSummaryType>))]
[Serializable]
public readonly record struct TransactionSummaryType : IStringEnum
{
    public static readonly TransactionSummaryType Capture = new(Values.Capture);

    public static readonly TransactionSummaryType Return = new(Values.Return);

    public TransactionSummaryType(string value)
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
    public static TransactionSummaryType FromCustom(string value)
    {
        return new TransactionSummaryType(value);
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

    public static bool operator ==(TransactionSummaryType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TransactionSummaryType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TransactionSummaryType value) => value.Value;

    public static explicit operator TransactionSummaryType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Capture = "capture";

        public const string Return = "return";
    }
}
