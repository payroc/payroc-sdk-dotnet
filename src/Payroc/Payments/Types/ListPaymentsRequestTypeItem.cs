using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.Payments;

[JsonConverter(typeof(StringEnumSerializer<ListPaymentsRequestTypeItem>))]
public readonly record struct ListPaymentsRequestTypeItem : IStringEnum
{
    public static readonly ListPaymentsRequestTypeItem Sale = Custom(Values.Sale);

    public static readonly ListPaymentsRequestTypeItem PreAuthorization = Custom(
        Values.PreAuthorization
    );

    public static readonly ListPaymentsRequestTypeItem PreAuthorizationCompletion = Custom(
        Values.PreAuthorizationCompletion
    );

    public ListPaymentsRequestTypeItem(string value)
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
    public static ListPaymentsRequestTypeItem Custom(string value)
    {
        return new ListPaymentsRequestTypeItem(value);
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

    public static bool operator ==(ListPaymentsRequestTypeItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentsRequestTypeItem value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string Sale = "sale";

        public const string PreAuthorization = "preAuthorization";

        public const string PreAuthorizationCompletion = "preAuthorizationCompletion";
    }
}
