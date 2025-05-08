using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CreateProcessingAccountBusinessType>))]
public readonly record struct CreateProcessingAccountBusinessType : IStringEnum
{
    public static readonly CreateProcessingAccountBusinessType Retail = new(Values.Retail);

    public static readonly CreateProcessingAccountBusinessType Restaurant = new(Values.Restaurant);

    public static readonly CreateProcessingAccountBusinessType Internet = new(Values.Internet);

    public static readonly CreateProcessingAccountBusinessType Moto = new(Values.Moto);

    public static readonly CreateProcessingAccountBusinessType Lodging = new(Values.Lodging);

    public static readonly CreateProcessingAccountBusinessType NotForProfit = new(
        Values.NotForProfit
    );

    public CreateProcessingAccountBusinessType(string value)
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
    public static CreateProcessingAccountBusinessType FromCustom(string value)
    {
        return new CreateProcessingAccountBusinessType(value);
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

    public static bool operator ==(CreateProcessingAccountBusinessType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateProcessingAccountBusinessType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateProcessingAccountBusinessType value) =>
        value.Value;

    public static explicit operator CreateProcessingAccountBusinessType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
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
