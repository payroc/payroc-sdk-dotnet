using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<ProcessingAccountBusinessType>))]
[Serializable]
public readonly record struct ProcessingAccountBusinessType : IStringEnum
{
    public static readonly ProcessingAccountBusinessType Retail = new(Values.Retail);

    public static readonly ProcessingAccountBusinessType Restaurant = new(Values.Restaurant);

    public static readonly ProcessingAccountBusinessType Internet = new(Values.Internet);

    public static readonly ProcessingAccountBusinessType Moto = new(Values.Moto);

    public static readonly ProcessingAccountBusinessType Lodging = new(Values.Lodging);

    public static readonly ProcessingAccountBusinessType NotForProfit = new(Values.NotForProfit);

    public ProcessingAccountBusinessType(string value)
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
    public static ProcessingAccountBusinessType FromCustom(string value)
    {
        return new ProcessingAccountBusinessType(value);
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

    public static bool operator ==(ProcessingAccountBusinessType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProcessingAccountBusinessType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProcessingAccountBusinessType value) => value.Value;

    public static explicit operator ProcessingAccountBusinessType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Retail = "retail";

        public const string Restaurant = "restaurant";

        public const string Internet = "internet";

        public const string Moto = "moto";

        public const string Lodging = "lodging";

        public const string NotForProfit = "notForProfit";
    }
}
