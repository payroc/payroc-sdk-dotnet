using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<BankTransferCustomerNotificationLanguage>))]
public readonly record struct BankTransferCustomerNotificationLanguage : IStringEnum
{
    public static readonly BankTransferCustomerNotificationLanguage En = Custom(Values.En);

    public static readonly BankTransferCustomerNotificationLanguage Fr = Custom(Values.Fr);

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
    public static BankTransferCustomerNotificationLanguage Custom(string value)
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

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string En = "en";

        public const string Fr = "fr";
    }
}
