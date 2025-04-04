using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestTender>))]
public readonly record struct ListPaymentsRequestTender : IStringEnum
{
    public static readonly ListPaymentsRequestTender Ebt = Custom(Values.Ebt);

    public static readonly ListPaymentsRequestTender CreditDebit = Custom(Values.CreditDebit);

    public ListPaymentsRequestTender(string value)
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
    public static ListPaymentsRequestTender Custom(string value)
    {
        return new ListPaymentsRequestTender(value);
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

    public static bool operator ==(ListPaymentsRequestTender value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentsRequestTender value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Ebt = "ebt";

        public const string CreditDebit = "creditDebit";
    }
}
