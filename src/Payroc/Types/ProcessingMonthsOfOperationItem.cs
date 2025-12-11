using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingMonthsOfOperationItem>))]
[Serializable]
public readonly record struct ProcessingMonthsOfOperationItem : IStringEnum
{
    public static readonly ProcessingMonthsOfOperationItem Jan = new(Values.Jan);

    public static readonly ProcessingMonthsOfOperationItem Feb = new(Values.Feb);

    public static readonly ProcessingMonthsOfOperationItem Mar = new(Values.Mar);

    public static readonly ProcessingMonthsOfOperationItem Apr = new(Values.Apr);

    public static readonly ProcessingMonthsOfOperationItem May = new(Values.May);

    public static readonly ProcessingMonthsOfOperationItem Jun = new(Values.Jun);

    public static readonly ProcessingMonthsOfOperationItem Jul = new(Values.Jul);

    public static readonly ProcessingMonthsOfOperationItem Aug = new(Values.Aug);

    public static readonly ProcessingMonthsOfOperationItem Sep = new(Values.Sep);

    public static readonly ProcessingMonthsOfOperationItem Oct = new(Values.Oct);

    public static readonly ProcessingMonthsOfOperationItem Nov = new(Values.Nov);

    public static readonly ProcessingMonthsOfOperationItem Dec = new(Values.Dec);

    public ProcessingMonthsOfOperationItem(string value)
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
    public static ProcessingMonthsOfOperationItem FromCustom(string value)
    {
        return new ProcessingMonthsOfOperationItem(value);
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

    public static bool operator ==(ProcessingMonthsOfOperationItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingMonthsOfOperationItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingMonthsOfOperationItem value) => value.Value;

    public static explicit operator ProcessingMonthsOfOperationItem(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Jan = "jan";

        public const string Feb = "feb";

        public const string Mar = "mar";

        public const string Apr = "apr";

        public const string May = "may";

        public const string Jun = "jun";

        public const string Jul = "jul";

        public const string Aug = "aug";

        public const string Sep = "sep";

        public const string Oct = "oct";

        public const string Nov = "nov";

        public const string Dec = "dec";
    }
}
