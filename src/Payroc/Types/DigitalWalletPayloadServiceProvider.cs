using System.Text.Json.Serialization;
using Payroc.Core;

namespace Payroc;

[JsonConverter(typeof(StringEnumSerializer<DigitalWalletPayloadServiceProvider>))]
[Serializable]
public readonly record struct DigitalWalletPayloadServiceProvider : IStringEnum
{
    public static readonly DigitalWalletPayloadServiceProvider Apple = new(Values.Apple);

    public static readonly DigitalWalletPayloadServiceProvider Google = new(Values.Google);

    public DigitalWalletPayloadServiceProvider(string value)
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
    public static DigitalWalletPayloadServiceProvider FromCustom(string value)
    {
        return new DigitalWalletPayloadServiceProvider(value);
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

    public static bool operator ==(DigitalWalletPayloadServiceProvider value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DigitalWalletPayloadServiceProvider value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DigitalWalletPayloadServiceProvider value) =>
        value.Value;

    public static explicit operator DigitalWalletPayloadServiceProvider(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Apple = "apple";

        public const string Google = "google";
    }
}
