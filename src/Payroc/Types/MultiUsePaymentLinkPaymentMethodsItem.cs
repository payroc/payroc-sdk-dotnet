using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(MultiUsePaymentLinkPaymentMethodsItem.MultiUsePaymentLinkPaymentMethodsItemSerializer)
)]
[Serializable]
public readonly record struct MultiUsePaymentLinkPaymentMethodsItem : IStringEnum
{
    public static readonly MultiUsePaymentLinkPaymentMethodsItem Card = new(Values.Card);

    public static readonly MultiUsePaymentLinkPaymentMethodsItem BankTransfer = new(
        Values.BankTransfer
    );

    public MultiUsePaymentLinkPaymentMethodsItem(string value)
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
    public static MultiUsePaymentLinkPaymentMethodsItem FromCustom(string value)
    {
        return new MultiUsePaymentLinkPaymentMethodsItem(value);
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

    public static bool operator ==(MultiUsePaymentLinkPaymentMethodsItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MultiUsePaymentLinkPaymentMethodsItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MultiUsePaymentLinkPaymentMethodsItem value) =>
        value.Value;

    public static explicit operator MultiUsePaymentLinkPaymentMethodsItem(string value) =>
        new(value);

    internal class MultiUsePaymentLinkPaymentMethodsItemSerializer
        : JsonConverter<MultiUsePaymentLinkPaymentMethodsItem>
    {
        public override MultiUsePaymentLinkPaymentMethodsItem Read(
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
            return new MultiUsePaymentLinkPaymentMethodsItem(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkPaymentMethodsItem value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MultiUsePaymentLinkPaymentMethodsItem ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new MultiUsePaymentLinkPaymentMethodsItem(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MultiUsePaymentLinkPaymentMethodsItem value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Card = "card";

        public const string BankTransfer = "bankTransfer";
    }
}
