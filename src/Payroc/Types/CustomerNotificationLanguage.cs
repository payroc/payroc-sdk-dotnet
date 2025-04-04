using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<CustomerNotificationLanguage>))]
public readonly record struct CustomerNotificationLanguage : IStringEnum
{
    public static readonly CustomerNotificationLanguage En = Custom(Values.En);

    public static readonly CustomerNotificationLanguage Fr = Custom(Values.Fr);

    public CustomerNotificationLanguage(string value)
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
    public static CustomerNotificationLanguage Custom(string value)
    {
        return new CustomerNotificationLanguage(value);
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

    public static bool operator ==(CustomerNotificationLanguage value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CustomerNotificationLanguage value1, string value2) =>
        !value1.Value.Equals(value2);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    public static class Values
    {
        public const string En = "en";

        public const string Fr = "fr";
    }
}
