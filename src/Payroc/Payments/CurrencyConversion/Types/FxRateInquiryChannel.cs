using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments.CurrencyConversion;

[JsonConverter(typeof(StringEnumSerializer<FxRateInquiryChannel>))]
public readonly record struct FxRateInquiryChannel : IStringEnum
{
    public static readonly FxRateInquiryChannel Pos = Custom(Values.Pos);

    public static readonly FxRateInquiryChannel Web = Custom(Values.Web);

    public static readonly FxRateInquiryChannel Moto = Custom(Values.Moto);

    public FxRateInquiryChannel(string value)
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
    public static FxRateInquiryChannel Custom(string value)
    {
        return new FxRateInquiryChannel(value);
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

    public static bool operator ==(FxRateInquiryChannel value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(FxRateInquiryChannel value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Pos = "pos";

        public const string Web = "web";

        public const string Moto = "moto";
    }
}
