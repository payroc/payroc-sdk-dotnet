using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<RefundSummaryResponseCode>))]
public readonly record struct RefundSummaryResponseCode : IStringEnum
{
    public static readonly RefundSummaryResponseCode A = Custom(Values.A);

    public static readonly RefundSummaryResponseCode D = Custom(Values.D);

    public static readonly RefundSummaryResponseCode E = Custom(Values.E);

    public static readonly RefundSummaryResponseCode P = Custom(Values.P);

    public static readonly RefundSummaryResponseCode R = Custom(Values.R);

    public static readonly RefundSummaryResponseCode C = Custom(Values.C);

    public RefundSummaryResponseCode(string value)
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
    public static RefundSummaryResponseCode Custom(string value)
    {
        return new RefundSummaryResponseCode(value);
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

    public static bool operator ==(RefundSummaryResponseCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RefundSummaryResponseCode value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string A = "A";

        public const string D = "D";

        public const string E = "E";

        public const string P = "P";

        public const string R = "R";

        public const string C = "C";
    }
}
