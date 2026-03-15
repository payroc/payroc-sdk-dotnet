using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc.PaymentLinks;

[JsonConverter(typeof(ListPaymentLinksRequestStatus.ListPaymentLinksRequestStatusSerializer))]
[Serializable]
public readonly record struct ListPaymentLinksRequestStatus : IStringEnum
{
    public static readonly ListPaymentLinksRequestStatus Active = new(Values.Active);

    public static readonly ListPaymentLinksRequestStatus Completed = new(Values.Completed);

    public static readonly ListPaymentLinksRequestStatus Deactivated = new(Values.Deactivated);

    public static readonly ListPaymentLinksRequestStatus Expired = new(Values.Expired);

    public ListPaymentLinksRequestStatus(string value)
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
    public static ListPaymentLinksRequestStatus FromCustom(string value)
    {
        return new ListPaymentLinksRequestStatus(value);
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

    public static bool operator ==(ListPaymentLinksRequestStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPaymentLinksRequestStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPaymentLinksRequestStatus value) => value.Value;

    public static explicit operator ListPaymentLinksRequestStatus(string value) => new(value);

    internal class ListPaymentLinksRequestStatusSerializer
        : JsonConverter<ListPaymentLinksRequestStatus>
    {
        public override ListPaymentLinksRequestStatus Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new ListPaymentLinksRequestStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            ListPaymentLinksRequestStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Active = "active";

        public const string Completed = "completed";

        public const string Deactivated = "deactivated";

        public const string Expired = "expired";
    }
}
