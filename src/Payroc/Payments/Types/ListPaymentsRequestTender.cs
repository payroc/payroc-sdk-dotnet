using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestTender>))]
[Serializable]
public readonly record struct ListPaymentsRequestTender : IStringEnum
{
    public static readonly ListPaymentsRequestTender Ebt = new(Values.Ebt);

    public static readonly ListPaymentsRequestTender CreditDebit = new(Values.CreditDebit);

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
    public static ListPaymentsRequestTender FromCustom(string value)
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

    public static explicit operator string(ListPaymentsRequestTender value) => value.Value;

    public static explicit operator ListPaymentsRequestTender(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Ebt = "ebt";

        public const string CreditDebit = "creditDebit";
    }
}
