using System.Text.Json;
using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(
    typeof(BankTransferCustomerNotificationLanguage.BankTransferCustomerNotificationLanguageSerializer)
)]
[Serializable]
public readonly record struct BankTransferCustomerNotificationLanguage : IStringEnum
{
    public static readonly BankTransferCustomerNotificationLanguage En = new(Values.En);

    public static readonly BankTransferCustomerNotificationLanguage Fr = new(Values.Fr);

    public BankTransferCustomerNotificationLanguage(string value)
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
    public static BankTransferCustomerNotificationLanguage FromCustom(string value)
    {
        return new BankTransferCustomerNotificationLanguage(value);
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

    public static bool operator ==(
        BankTransferCustomerNotificationLanguage value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        BankTransferCustomerNotificationLanguage value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(BankTransferCustomerNotificationLanguage value) =>
        value.Value;

    public static explicit operator BankTransferCustomerNotificationLanguage(string value) =>
        new(value);

    internal class BankTransferCustomerNotificationLanguageSerializer
        : JsonConverter<BankTransferCustomerNotificationLanguage>
    {
        public override BankTransferCustomerNotificationLanguage Read(
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
            return new BankTransferCustomerNotificationLanguage(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            BankTransferCustomerNotificationLanguage value,
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
        public const string En = "en";

        public const string Fr = "fr";
    }
}
